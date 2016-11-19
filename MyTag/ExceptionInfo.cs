using System;
using System.Diagnostics;
using System.Linq;

public class ExceptionInfo
{
	public ExceptionInfo()
	{
	}

    public static string ShowExceptionInfo(Exception ex)
    {
        StackTrace st = new StackTrace(ex, true); // create the stack trace
        var query = st.GetFrames()         // get the frames
                      .Select(frame => new
                      {                   // get the info
                          FileName = frame.GetFileName(),
                          LineNumber = frame.GetFileLineNumber(),
                          ColumnNumber = frame.GetFileColumnNumber(),
                          Method = frame.GetMethod(),
                          Class = frame.GetMethod().DeclaringType,
                      });
        // log the information obtained from the query

        var FileName = query.First().FileName;
        var LineNumber = query.First().LineNumber;
        var ColumnNumber = query.First().ColumnNumber;
        var Method = query.First().Method;
        var Class = query.First().Class;

        string ExceptionInfo = "FileName=" + FileName + ","
            + "LineNumber=" + LineNumber + ","
            + "Method=" + Method + ","
            + "Class=" + Class
            + ex.Message;

        return (ExceptionInfo);
    }

}
