﻿/*
 * I hereby release this file, StreamExtensions.cs, to the public domain.
 * Use it if you wish.
 */
using System;
using System.Text;
using System.IO;
using System.Buffers.Binary;
using System.Security.Cryptography.X509Certificates;

namespace GameArchives
{
  internal static class StreamExtensions
  {
    /// <summary>
    /// Read a signed 8-bit integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static sbyte ReadInt8(this Stream s) => unchecked((sbyte)s.ReadUInt8());

    /// <summary>
    /// Read an unsigned 8-bit integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte ReadUInt8(this Stream s)
    {
      byte ret;
      byte[] tmp = new byte[1];
      s.Read(tmp, 0, 1);
      ret = tmp[0];
      return ret;
    }

    /// <summary>
    /// Read an unsigned 16-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ushort ReadUInt16LE(this Stream s) => unchecked((ushort)s.ReadInt16LE());

    /// <summary>
    /// Read a signed 16-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static short ReadInt16LE(this Stream s)
    {
      return SafeRead<short>(s, 2, (v) => BinaryPrimitives.ReadInt16LittleEndian(v));
    }

    /// <summary>
    /// Read an unsigned 16-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ushort ReadUInt16BE(this Stream s) => unchecked((ushort)s.ReadInt16BE());

    /// <summary>
    /// Read a signed 16-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static short ReadInt16BE(this Stream s)
    {
      return SafeRead<short>(s, 2, (v) => BinaryPrimitives.ReadInt16BigEndian(v));

    }

    /// <summary>
    /// Read an unsigned 24-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadUInt24LE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[0] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[2] << 16) & 0xFF0000;
      return ret;
    }

    /// <summary>
    /// Read a signed 24-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt24LE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[0] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[2] << 16) & 0xFF0000;
      if ((tmp[2] & 0x80) == 0x80)
      {
        ret |= 0xFF << 24;
      }
      return ret;
    }

    /// <summary>
    /// Read an unsigned 24-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static uint ReadUInt24BE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[2] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[0] << 16) & 0xFF0000;
      return (uint)ret;
    }

    /// <summary>
    /// Read a signed 24-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt24BE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[2] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[0] << 16) & 0xFF0000;
      if ((tmp[0] & 0x80) == 0x80)
      {
        ret |= 0xFF << 24; // sign-extend
      }
      return ret;
    }

    /// <summary>
    /// Read an unsigned 32-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static uint ReadUInt32LE(this Stream s) => unchecked((uint)s.ReadInt32LE());

    /// <summary>
    /// Read a signed 32-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt32LE(this Stream s)
    {
      return SafeRead<int>(s, 4, (v) => BinaryPrimitives.ReadInt32LittleEndian(v));
    }
      
    /// <summary>
    /// Read an unsigned 32-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static uint ReadUInt32BE(this Stream s) => unchecked((uint)s.ReadInt32BE());

    /// <summary>
    /// Read a signed 32-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt32BE(this Stream s)
    {
      return SafeRead<int>(s, 4, (v) => BinaryPrimitives.ReadInt32BigEndian(v));
    }

    /// <summary>
    /// Read an unsigned 64-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ulong ReadUInt64LE(this Stream s) => unchecked((ulong)s.ReadInt64LE());

    /// <summary>
    /// Read a signed 64-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ReadInt64LE(this Stream s)
    {
      return SafeRead<long>(s, 8, (v) => BinaryPrimitives.ReadInt64LittleEndian(v));
    }

    /// <summary>
    /// Read an unsigned 64-bit big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ulong ReadUInt64BE(this Stream s) => unchecked((ulong)s.ReadInt64BE());

    /// <summary>
    /// Read a signed 64-bit big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ReadInt64BE(this Stream s)
    {
      return SafeRead<long>(s, 8, (v) => BinaryPrimitives.ReadInt64BigEndian(v));
    }

    /// <summary>
    /// Reads a multibyte value of the specified length from the stream.
    /// </summary>
    /// <param name="s">The stream</param>
    /// <param name="bytes">Must be less than or equal to 8</param>
    /// <returns></returns>
    public static long ReadMultibyteBE(this Stream s, byte bytes)
    {
      if (bytes > 8) return 0;
      long ret = 0;

      var b = s.ReadBytes(bytes);
      for(uint i = 0; i < b.Length; i++)
      {
        ret <<= 8;
        ret |= b[i];
      }
      return ret;
    }

    public static T SafeRead<T>(Stream s, int bytesToRead, Func<byte[], T> f) // Takes advantage of the BinaryPrimitives reader.
    {
      using var reader = new BinaryReader(s, Encoding.UTF8, leaveOpen: true);
      byte[] tmp = reader.ReadBytes(bytesToRead);

      if (tmp.Length >= bytesToRead)
      {
        return f(tmp);
      }
      else
      {
        return default; // Mainly a debugging precaution.
      }
    }

    /// <summary>
    /// Read a single-precision (4-byte) floating-point value from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static float ReadFloat(this Stream s)
    {
      byte[] tmp = new byte[4];
      s.Read(tmp, 0, 4);
      return BitConverter.ToSingle(tmp, 0);
    }

    /// <summary>
    /// Read a null-terminated ASCII string from the given stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadASCIINullTerminated(this Stream s, int limit = -1)
    {
      StringBuilder sb = new StringBuilder(255);
      char cur;
      while ((limit == -1 || sb.Length < limit) && (cur = (char)s.ReadByte()) != 0)
      {
        sb.Append(cur);
      }
      return sb.ToString();
    }

    /// <summary>
    /// Read a length-prefixed string of the specified encoding type from the file.
    /// The length is a 32-bit little endian integer.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e">The encoding to use to decode the string.</param>
    /// <returns></returns>
    public static string ReadLengthPrefixedString(this Stream s, Encoding e)
    {
      int length = s.ReadInt32LE();
      byte[] chars = new byte[length];
      s.Read(chars, 0, length);
      return e.GetString(chars);
    }

    /// <summary>
    /// Read a length-prefixed UTF-8 string from the given stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadLengthUTF8(this Stream s)
    {
      return s.ReadLengthPrefixedString(Encoding.UTF8);
    }

    /// <summary>
    /// Read a given number of bytes from a stream into a new byte array.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="count">Number of bytes to read (maximum)</param>
    /// <returns>New byte array of size &lt;=count.</returns>
    public static byte[] ReadBytes(this Stream s, int count)
    {
      // Size of returned array at most count, at least difference between position and length.
      int realCount = (int)((s.Position + count > s.Length) ? (s.Length - s.Position) : count);
      byte[] ret = new byte[realCount];
      s.Read(ret, 0, realCount);
      return ret;
    }

    /// <summary>
    /// Read a variable-length integral value as found in MIDI messages.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadMidiMultiByte(this Stream s)
    {
      int ret = 0;
      byte b = (byte)(s.ReadByte());
      ret += b & 0x7f;
      if (0x80 == (b & 0x80))
      {
        ret <<= 7;
        b = (byte)(s.ReadByte());
        ret += b & 0x7f;
        if (0x80 == (b & 0x80))
        {
          ret <<= 7;
          b = (byte)(s.ReadByte());
          ret += b & 0x7f;
          if (0x80 == (b & 0x80))
          {
            ret <<= 7;
            b = (byte)(s.ReadByte());
            ret += b & 0x7f;
            if (0x80 == (b & 0x80))
              throw new InvalidDataException("Variable-length MIDI number > 4 bytes");
          }
        }
      }
      return ret;
    }
  }
}
