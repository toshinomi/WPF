using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 共通関数のロジック
/// </summary>
public class ComFunc
{
    /// <summary>
    /// Double型のデータからbyte型のデータへの変換
    /// </summary>
    /// <param name="_dValue">Double型のデータ</param>
    /// <returns>byte型のデータ</returns>
    static public byte DoubleToByte(double _dValue)
    {
        byte nCnvValue = 0;

        if (_dValue > 255.0)
        {
            nCnvValue = 255;
        }
        else if (_dValue < 0)
        {
            nCnvValue = 0;
        }
        else
        {
            nCnvValue = (byte)_dValue;
        }

        return nCnvValue;
    }

    /// <summary>
    /// long型のデータからbyte型のデータへの変換
    /// </summary>
    /// <param name="_lValue">long型のデータ</param>
    /// <returns>byte型のデータ</returns>
    static public byte LongToByte(long _lValue)
    {
        byte nCnvValue = 0;

        if (_lValue > 255)
        {
            nCnvValue = 255;
        }
        else if (_lValue < 0)
        {
            nCnvValue = 0;
        }
        else
        {
            nCnvValue = (byte)_lValue;
        }

        return nCnvValue;
    }
}