using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Json5.Parsing
{
  /// <summary>
  /// Converts a stream of JSON5 tokens into a <see cref="Json5Value"/>.
  /// </summary>
  class Json5Parser
  {
    private Json5Lexer lexer;
    private State state;
    private Json5Value root;
    private Stack<Json5Container> stack;
    private Json5Container currentContainer;
    private bool parsed = false;

    /// <summary>
    /// Constructs a new <see cref="Json5Parser"/> using a<see cref="TextReader"/>.
    /// </summary>
    /// <param name="reader">The reader that reads JSON5 text.</param>
    public Json5Parser(TextReader reader)
    {
      this.lexer = new Json5Lexer(reader);
    }

    /// <summary>
    /// Parses and returns a <see cref="Json5Value"/>.
    /// </summary>
    /// <returns>A <see cref="Json5Value"/>.</returns>
    public Json5Value Parse()
    {
      if(this.parsed)
        throw new InvalidOperationException("A Json5Parser may only be used once.");

      this.state = State.Value;
      this.root = null;
      this.stack = new Stack<Json5Container>();
      this.currentContainer = null;
      this.parsed = true;

      string key = null;

      start:
      Json5Token token = this.lexer.Read();

      switch(this.state)
      {
        case State.Value:
          switch(token.Type)
          {
            case Json5TokenType.String:
              this.Add(new Json5String(token.String), key);
              goto start;

            case Json5TokenType.Number:
              this.Add(new Json5Number(token.Number), key);
              goto start;

            case Json5TokenType.Punctuator:
              switch(token.Character)
              {
                case '[':
                  this.Add(new Json5Array(), key);
                  goto start;

                case '{':
                  this.Add(new Json5Object(), key);
                  goto start;
              }

              throw UnexpectedToken(token);

            case Json5TokenType.Identifier:
              // Since these are literal tokens, check token.Input
              // instead of token.String. Whereas `true` and `\x74rue`
              // are both valid identifiers that represent the same
              // thing, only `true` is a boolean literal.
              switch(token.Input)
              {
                case "true":
                  this.Add(new Json5Boolean(true), key);
                  goto start;

                case "false":
                  this.Add(new Json5Boolean(false), key);
                  goto start;

                case "null":
                  this.Add(Json5Value.Null, key);
                  goto start;

                case "Infinity":
                  this.Add(new Json5Number(double.PositiveInfinity), key);
                  goto start;

                case "NaN":
                  this.Add(new Json5Number(double.NaN), key);
                  goto start;
              }

              break;
          }

          break;

        case State.BeforeArrayElement:
          if(token.Type == Json5TokenType.Punctuator && token.Character == ']')
          {
            this.Pop();
            goto start;
          }

          this.state = State.Value;
          goto case State.Value;

        case State.AfterArrayElement:
          if(token.Type == Json5TokenType.Punctuator)
          {
            switch(token.Character)
            {
              case ',':
                this.state = State.BeforeArrayElement;
                goto start;

              case ']':
                this.Pop();
                goto start;
            }
          }

          break;

        case State.BeforeObjectKey:
          switch(token.Type)
          {
            case Json5TokenType.Punctuator:
              if(token.Character == '}')
              {
                this.Pop();
                goto start;
              }

              break;

            case Json5TokenType.Identifier:
            case Json5TokenType.String:
              // All identifiers are valid as keys
              // even if they are literals like
              // `true`, `null`, or `Infinity`.
              key = token.String;
              this.state = State.AfterObjectKey;
              goto start;
          }

          break;

        case State.AfterObjectKey:
          if(token.Type != Json5TokenType.Punctuator || token.Character != ':')
            break;

          this.state = State.Value;
          goto start;

        case State.AfterObjectValue:
          if(token.Type == Json5TokenType.Punctuator)
          {
            switch(token.Character)
            {
              case ',':
                this.state = State.BeforeObjectKey;
                goto start;

              case '}':
                this.Pop();
                goto start;
            }
          }

          break;

        case State.End:
          if(token.Type == Json5TokenType.Eof)
            return this.root;

          break;
      }

      throw UnexpectedToken(token);
      //throw new Exception("Invalid tree state.");
    }

    /// <summary>
    /// Pushes a container onto the stack of open containers.
    /// </summary>
    /// <param name="container">The container to push onto the stack of open containers.</param>
    /// <remarks>This also sets the current container and changes the state of the parser if appropriate.</remarks>
    void Push(Json5Container container)
    {
      //if(this.root == null)
      //  this.root = container;

      this.stack.Push(container);
      this.currentContainer = container;

      if(this.currentContainer is Json5Array)
        this.state = State.BeforeArrayElement;
      else
        this.state = State.BeforeObjectKey;
    }

    /// <summary>
    /// Pops a container off the stack of open containers, sets the current container, and resets the state of the
    /// parser.
    /// </summary>
    /// <remarks>This also sets the current container and resets the state of the parser.</remarks>
    void Pop()
    {
      this.stack.Pop();
      this.currentContainer = this.stack.LastOrDefault();
      this.ResetState();
    }

    /// <summary>
    /// Adds a value to the current container, and uses the given key if the current container is an object.
    /// </summary>
    /// <param name="value">The value to add to the current container.</param>
    /// <param name="key">The key to use if the current container is an object.</param>
    /// <remarks>This also pushes <paramref name="value"/> onto the stack of open containers if
    /// <paramref name="value"/> is a container; otherwise, it reset the state of the parser.</remarks>
    void Add(Json5Value value, string key)
    {
      if(this.root == null)
        this.root = value;

      if(this.currentContainer is Json5Array)
        ((Json5Array)this.currentContainer).Add(value);
      else if(this.currentContainer is Json5Object)
        ((Json5Object)this.currentContainer)[key] = value;

      if(value is Json5Container)
        this.Push((Json5Container)value);
      else
        this.ResetState();
    }

    /// <summary>
    /// Resets the state of the parser based on the current container.
    /// </summary>
    void ResetState()
    {
      if(this.currentContainer == null)
        this.state = State.End;
      else if(this.currentContainer is Json5Array)
        this.state = State.AfterArrayElement;
      else
        this.state = State.AfterObjectValue;
    }

    /// <summary>
    /// Returns a <see cref="SyntaxError"/> for an unexpected token.
    /// </summary>
    /// <param name="token">The unexpected token.</param>
    /// <returns>A <see cref="SyntaxError"/> for <paramref name="token"/>.</returns>
    Exception UnexpectedToken(Json5Token token)
    {
      if(token.Type == Json5TokenType.Eof)
        return new Json5UnexpectedEndOfInputException(token.Line, token.Column);

      return new Json5UnexpectedInputException(token.Input, token.Line, token.Column);
    }

    /// <summary>
    /// Represents a state that a <see cref="Json5Parser"/> is in.
    /// </summary>
    enum State
    {
      BeforeArrayElement,
      AfterArrayElement,
      BeforeObjectKey,
      AfterObjectKey,
      AfterObjectValue,
      Value,
      End,
    }
  }
}
