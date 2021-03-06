using System;
using System.Text;
using System.Threading;

namespace YukariToolBox.Console
{
    /// <summary>
    /// 格式化的控制台日志输出
    /// </summary>
    public class ConsoleLog
    {
        #region Log等级设置
        private static LogLevel level = LogLevel.Info;
        
        /// <summary>
        /// <para>设置日志等级</para>
        /// <para>如需禁用log请使用<see cref="SetNoLog"/></para>
        /// </summary>
        /// <param name="level">LogLevel</param>
        /// <exception cref="ArgumentOutOfRangeException">loglevel超出正常值</exception>
        public static void SetLogLevel(LogLevel level)
        {
            if (level is < LogLevel.Debug or > LogLevel.Fatal)
                throw new ArgumentOutOfRangeException(nameof(level), "loglevel out of range");
            level = level;
        }

        /// <summary>
        /// 禁用log
        /// </summary>
        public static void SetNoLog() => level = (LogLevel) 5;
        #endregion

        #region 输出服务提供者设置
        /// <summary>
        /// 输出服务
        /// </summary>
        private static IConsoleLogService logger = new YukariConsoleLoggerService();

        /// <summary>
        /// 设置控制台输出服务
        /// </summary>
        /// <param name="logger">新的控制台输出服务</param>
        public static void SetLoggerService(IConsoleLogService logger)
        {
            logger = logger;
        }
        #endregion

        #region 格式化错误Log
        /// <summary>
        /// 生成格式化的错误Log文本
        /// </summary>
        /// <param name="e">错误</param>
        /// <returns>格式化Log</returns>
        public static string ErrorLogBuilder(Exception e)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();
            errorMessageBuilder.Append("\r\n");
            errorMessageBuilder.Append("==============ERROR==============\r\n");
            errorMessageBuilder.Append("Error:");
            errorMessageBuilder.Append(e.GetType().FullName);
            errorMessageBuilder.Append("\r\n\r\n");
            errorMessageBuilder.Append("Message:");
            errorMessageBuilder.Append(e.Message);
            errorMessageBuilder.Append("\r\n\r\n");
            errorMessageBuilder.Append("Stack Trace:\r\n");
            errorMessageBuilder.Append(e.StackTrace);
            errorMessageBuilder.Append("\r\n");
            errorMessageBuilder.Append("=================================\r\n");
            return errorMessageBuilder.ToString();
        }
        #endregion

        #region 格式化控制台Log函数
        /// <summary>
        /// 向控制台发送Info信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Info(object type, object message)
        {
            if (level > LogLevel.Info) return;
            logger.Info(type, message);
        }

        /// <summary>
        /// 向控制台发送Warning信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Warning(object type, object message)
        {
            if (level > LogLevel.Warn) return;
            logger.Warning(type, message);
        }

        /// <summary>
        /// 向控制台发送Error信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Error(object type, object message)
        {
            if (level > LogLevel.Error) return;
            logger.Error(type, message);
        }

        /// <summary>
        /// 向控制台发送Fatal信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Fatal(object type, object message)
        {
            if (level > LogLevel.Fatal) return;
            logger.Fatal(type, message);
        }

        /// <summary>
        /// 向控制台发送Debug信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Debug(object type, object message)
        {
            if (level != LogLevel.Debug) return;
            logger.Fatal(type, message);
        }
        #endregion

        #region 全局错误Log
        /// <summary>
        /// 全局错误Log
        /// </summary>
        /// <param name="args">UnhandledExceptionEventArgs</param>
        public static void UnhandledExceptionLog(UnhandledExceptionEventArgs args)
        {
            logger.UnhandledExceptionLog(args);
        }
        #endregion
    }
}
