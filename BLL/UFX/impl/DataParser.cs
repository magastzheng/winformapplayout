using hundsun.t2sdk;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UFX.impl
{
    
    public class DataParser
    {
        private List<RawDataSet> _dataSets = new List<RawDataSet>();

        public List<RawDataSet> DataSets
        {
            get { return _dataSets; }
        }

        public void Parse(CT2UnPacker lpUnPack)
        {
            for (int i = 0; i < lpUnPack.GetDatasetCount(); i++)
            {
                RawDataSet dataSet = new RawDataSet();
                dataSet.Rows = new List<RawDataRow>();
                //设置当前结果集
                lpUnPack.SetCurrentDatasetByIndex(i);

                Dictionary<int, string> columnDic = new Dictionary<int, string>();
                //打印字段
                for (int j = 0; j < lpUnPack.GetColCount(); j++)
                {
                    columnDic.Add(j, lpUnPack.GetColName(j));
                }

                //打印所有记录
                for (int k = 0; k < lpUnPack.GetRowCount(); k++)
                {
                    RawDataRow row = new RawDataRow();
                    row.Columns = new Dictionary<string,DataValue>();

                    //打印每条记录
                    for (int t = 0; t < lpUnPack.GetColCount(); t++)
                    {
                        string colName = columnDic[t];
                        switch (lpUnPack.GetColType(t))
                        {
                            case (sbyte)'I':  //I 整数
                                {
                                    DataValue dataValue = new DataValue 
                                    {
                                        Type = DataValueType.Int,
                                        Value = lpUnPack.GetIntByIndex(t)
                                    };


                                    row.Columns.Add(colName, dataValue);
                                }
                                break;
                            case (sbyte)'C':  //C 
                                {
                                    DataValue dataValue = new DataValue
                                    {
                                        Type = DataValueType.Char,
                                        Value = lpUnPack.GetCharByIndex(t)
                                    };


                                    row.Columns.Add(colName, dataValue);
                                }
                                break;
                            case (sbyte)'S':   //S
                                {
                                    DataValue dataValue = new DataValue
                                    {
                                        Type = DataValueType.String,
                                        Value = lpUnPack.GetStrByIndex(t)
                                    };


                                    row.Columns.Add(colName, dataValue);
                                }
                                break;
                            case (sbyte)'F':  //F
                                {
                                    DataValue dataValue = new DataValue
                                    {
                                        Type = DataValueType.Float,
                                        Value = lpUnPack.GetDoubleByIndex(t)
                                    };


                                    row.Columns.Add(colName, dataValue);
                                }
                                break;
                            case (sbyte)'R':  //R
                                {
                                    break;
                                }
                            default:
                                // 未知数据类型
                                break;
                        }
                    }//end to read all column for each row

                    dataSet.Rows.Add(row);

                    Console.WriteLine();
                    lpUnPack.Next();
                }//end to read rows

                _dataSets.Add(dataSet);
            }

            /*
            while (lpUnPack.IsEOF() != 1)
            {
                for (int i = 0; i < lpUnPack.GetColCount(); i++)
                {
                    String colName = lpUnPack.GetColName(i);
                    sbyte colType = lpUnPack.GetColType(i);
                    if (!colType.Equals('R'))
                    {
                        String colValue = lpUnPack.GetStrByIndex(i);
                        Console.WriteLine("{0}：{1}", colName, colValue);
                    }
                    else
                    {
                        int colLength = 0;
                        unsafe
                        {
                            void* colValue = (char*)lpUnPack.GetRawByIndex(i, &colLength);
                            string str = String.Format("{0}:[{1}]({2})", colName, Marshal.PtrToStringAuto(new IntPtr(colValue)), colLength);
                        }
                    }
                }
                lpUnPack.Next();
            }
            */

        }

        public void Output()
        {
            for(int k = 0; k < 1; k++)
            {
                var dataSet = _dataSets[k];
                if (dataSet == null || dataSet.Rows == null || dataSet.Rows.Count == 0)
                    return;

                for (int i = 0; i < 1; i++)
                {
                    var header = dataSet.Rows[0];
                    string strName = string.Empty, strValue = string.Empty;
                    foreach (var kv in header.Columns)
                    {
                        strName += kv.Key;
                        strName += "\t";
                        strValue += kv.Value.Value.ToString();
                        strValue += "\t";
                    }
                    Console.WriteLine(strName);
                    Console.WriteLine(strValue);
                }
            }

            for(int k = 1; k < _dataSets.Count; k++)
            {
                var dataSet = _dataSets[k];
                string strDataName = string.Empty;
                for (int i = 0; i < dataSet.Rows.Count; i++)
                {
                    var row = dataSet.Rows[i];
                    string strDataValue = string.Empty;
                    foreach (var kv in row.Columns)
                    {
                        if (i == 0)
                        {
                            strDataName += kv.Key;
                            strDataName += "\t";
                        }
                        
                        strDataValue += kv.Value.Value.ToString();
                        strDataValue += "\t";
                    }
                    if (i == 0)
                    {
                        Console.WriteLine(strDataName);
                    }
                    
                    Console.WriteLine(strDataValue);
                }
            }
        }
    }
}
