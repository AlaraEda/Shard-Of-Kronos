using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for Debug commands. It only contains the command metadata
/// and doesn't have functionality for executing anything
/// </summary>
public class DebugCommandBase
{
    // The name of the command itself
    private string _commandId;
    // Description that explains what the command does
    private string _commandDescription;
    // The parameter format for the command e.g. 'speed <speed_value>'
    private string _commandFormat;
    
    /**
     * Getters for the private fields above
     */
    public string commandId { get { return _commandId; } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat { get { return _commandFormat; } }

    public DebugCommandBase(string id,string description, string format)
    {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
    }
}

/// <summary>
/// Class representing a debug command with a strongly typed parameter
/// </summary>
/// <typeparam name="T1">The type of value that is passed as argument to the command</typeparam>
public class DebugCommand<T1> : DebugCommandBase
{
    // The function to call when command is executed
    private Action<T1> command;
    public DebugCommand(string id,string description, string format, Action<T1> command):base(id, description,format)
    {
        this.command = command;
    }
    
    /// <summary>
    /// Invokes the command by calling the command field with the supplied value
    /// </summary>
    /// <param name="value">The parameter to be passed to the command function</param>
    public void InvokeAction(T1 value)
    {
        command.Invoke(value);
    }
  
}

/// <summary>
/// Class representing a debug command without any parameters
/// </summary>
public class DebugCommand : DebugCommandBase
{
    // The function to call when command is executed
    private Action command;
    public DebugCommand(string id, string description, string format, Action command) : base(id, description, format)
    {
        this.command = command;
    }
    /// <summary>
    /// Invokes the command by calling the command field function
    /// </summary>
    public void InvokeAction()
    {
        command.Invoke();
    }

}
