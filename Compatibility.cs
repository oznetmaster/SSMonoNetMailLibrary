using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace System
	{
	[Flags]
	public enum Base64FormattingOptions
		{
		None,
		InsertLineBreaks
		}

	public static class ConvertEx
		{
		private const int MaxBytesPerLine = 57;

		public static string ToBase64String (byte[] inArray, Base64FormattingOptions options)
			{
			return ToBase64String (inArray, 0, inArray.Length, options);
			}

		public static string ToBase64String (byte[] inArray, int offset, int length,  Base64FormattingOptions options)
			{
			var s64 = Convert.ToBase64String (inArray, offset, length);
			if (options == Base64FormattingOptions.None)
				return s64;

			length = s64.Length;
			var sb = new StringBuilder ();
			int remainder = length % MaxBytesPerLine;
			int full = length / MaxBytesPerLine;
			offset = 0;
			for (int i = 0; i < full; i++)
				{
				sb.AppendLine (s64.Substring (offset, MaxBytesPerLine));
				offset += MaxBytesPerLine;
				}			// we never complete (i.e. the last line) with a new line
			if (remainder == 0)
				{
				int nll = CrestronEnvironment.NewLine.Length;
				sb.Remove (sb.Length - nll, nll);
				}
			else
				sb.Append (s64.Substring (offset));
			return sb.ToString ();
			}
		}
	}