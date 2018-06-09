﻿///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.compat.logging;

namespace com.espertech.esper.compat.logger
{
    using LogManager = com.espertech.esper.compat.logging.LogManager;

    /// <summary>
    /// A logger implementation based on NLog.
    /// </summary>
    /// <seealso cref="com.espertech.esper.compat.logging.ILog" />
    public class LoggerNLog : ILog
    {
        private readonly NLog.ILogger _log;

        /// <summary>
        /// Registers this logger.
        /// </summary>
        public static void Register()
        {
            LogManager.FactoryLoggerFromType = type =>
                new LoggerNLog(NLog.LogManager.GetLogger(type.FullName));
            LogManager.FactoryLoggerFromName = name =>
                new LoggerNLog(NLog.LogManager.GetLogger(name));
        }

        /// <summary>
        /// A simple configuration for "basic" setups - mostly testing.
        /// </summary>
        public static void BasicConfig()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var console = new NLog.Targets.ConsoleTarget();
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, console);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerNLog"/> class.
        /// </summary>
        /// <param name="log">The log.</param>
        public LoggerNLog(NLog.ILogger log)
        {
            _log = log;
            ChangeConfiguration();
        }

        /// <summary>
        /// Changes the configuration.
        /// </summary>
        private void ChangeConfiguration()
        {
            IsDebugEnabled = _log.IsDebugEnabled;
            IsInfoEnabled = _log.IsInfoEnabled;
            IsWarnEnabled = _log.IsWarnEnabled;
            IsErrorEnabled = _log.IsErrorEnabled;
            IsFatalEnabled = _log.IsFatalEnabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance severity for debug is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsDebugEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance severity for info is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is information enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsInfoEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance severity for warning is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is warn enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsWarnEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance severity for error is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsErrorEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance severity for fatal is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fatal enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsFatalEnabled { get; set; }

        /// <summary>
        /// Writes the specified message to the logger at debug severity.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at debug severity.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">The arguments.</param>
        public void Debug(string messageFormat, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(messageFormat, args);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at debug severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Debug(string message, Exception e)
        {
            if (IsDebugEnabled)
            {
                _log.Debug(e, message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at info severity.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            if (IsInfoEnabled)
            {
                _log.Info(message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at info severity.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">The arguments.</param>
        public void Info(string messageFormat, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _log.Info(messageFormat, args);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at info severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Info(string message, Exception e)
        {
            if (IsInfoEnabled)
            {
                _log.Info(e, message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at warning severity.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at warning severity.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">The arguments.</param>
        public void Warn(string messageFormat, params object[] args)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(messageFormat, args);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at warning severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Warn(string message, Exception e)
        {
            if (IsWarnEnabled)
            {
                _log.Warn(e, message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at error severity.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            if (IsErrorEnabled)
            {
                _log.Error(message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at error severity.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">The arguments.</param>
        public void Error(string messageFormat, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _log.Error(messageFormat, args);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at error severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Error(string message, Exception e)
        {
            if (IsErrorEnabled)
            {
                _log.Error(e, message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at fatal severity.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(message);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at fatal severity.
        /// </summary>
        /// <param name="messageFormat">The message format.</param>
        /// <param name="args">The arguments.</param>
        public void Fatal(string messageFormat, params object[] args)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(messageFormat, args);
            }
        }

        /// <summary>
        /// Writes the specified message to the logger at fatal severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Fatal(string message, Exception e)
        {
            if (IsFatalEnabled)
            {
                _log.Fatal(e, message);
            }
        }

    }
}
