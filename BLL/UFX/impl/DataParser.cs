using hundsun.t2sdk;
using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.UFX.impl
{
    
    public class DataParser
    {
        private List<RawDataSet> _dataSets = new List<RawDataSet>();

        public List<RawDataSet> DataSets
        {
            get { return _dataSets; }
        }

        public ConnectionCode ErrorCode { get; set; }

        public FunctionCode FunctionCode { get; set; } 

        public void Parse(CT2UnPacker lpUnPack)
        {
            for (int i = 0, dsLen = lpUnPack.GetDatasetCount(); i < dsLen; i++)
            {
                RawDataSet dataSet = new RawDataSet();
                dataSet.Rows = new List<RawDataRow>();
                //设置当前结果集
                lpUnPack.SetCurrentDatasetByIndex(i);

                Dictionary<int, string> columnDic = new Dictionary<int, string>();
                //打印字段
                for (int j = 0, hLen = lpUnPack.GetColCount(); j < hLen; j++)
                {
                    columnDic.Add(j, lpUnPack.GetColName(j));
                }

                //打印所有记录
                for (int k = 0, rLen = (int)lpUnPack.GetRowCount(); k < rLen; k++)
                {
                    RawDataRow row = new RawDataRow();
                    row.Columns = new Dictionary<string,DataValue>();

                    //打印每条记录
                    for (int t = 0, cLen = lpUnPack.GetColCount(); t < cLen; t++)
                    {
                        string colName = columnDic[t];
                        DataValue dataValue = new DataValue();
                        switch (lpUnPack.GetColType(t))
                        {
                            case (sbyte)'I':  //I 整数
                                {
                                    dataValue.Type = DataValueType.Int;
                                    dataValue.Value = lpUnPack.GetIntByIndex(t);
                                }
                                break;
                            case (sbyte)'C':  //C 
                                {
                                   dataValue.Type = DataValueType.Char;
                                   dataValue.Value = lpUnPack.GetCharByIndex(t);
                                }
                                break;
                            case (sbyte)'S':   //S
                                {
                                    dataValue.Type = DataValueType.String;
                                    dataValue.Value = lpUnPack.GetStrByIndex(t);
                                }
                                break;
                            case (sbyte)'F':  //F
                                {
                                    dataValue.Type = DataValueType.Float;
                                    dataValue.Value = lpUnPack.GetDoubleByIndex(t);
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

                        if (!row.Columns.ContainsKey(colName))
                        {
                            row.Columns.Add(colName, dataValue);
                        }
                    }//end to read all column for each row

                    dataSet.Rows.Add(row);

                    Console.WriteLine();
                    lpUnPack.Next();
                }//end to read rows

                _dataSets.Add(dataSet);
            }
        }

        public void Output()
        {
            //for(int k = 0; k < 1; k++)
            //{
            //    var dataSet = _dataSets[k];
            //    if (dataSet == null || dataSet.Rows == null || dataSet.Rows.Count == 0)
            //        return;

            //    for (int i = 0; i < 1; i++)
            //    {
            //        var header = dataSet.Rows[0];
            //        string strName = string.Empty, strValue = string.Empty;
            //        foreach (var kv in header.Columns)
            //        {
            //            strName += kv.Key;
            //            strName += "\t";
            //            strValue += kv.Value.Value.ToString();
            //            strValue += "\t";
            //        }
            //        Console.WriteLine(strName);
            //        Console.WriteLine(strValue);
            //    }
            //}

            //for(int k = 1; k < _dataSets.Count; k++)
            //{
            //    var dataSet = _dataSets[k];
            //    string strDataName = string.Empty;
            //    for (int i = 0; i < dataSet.Rows.Count; i++)
            //    {
            //        var row = dataSet.Rows[i];
            //        string strDataValue = string.Empty;
            //        foreach (var kv in row.Columns)
            //        {
            //            if (i == 0)
            //            {
            //                strDataName += kv.Key;
            //                strDataName += "\t";
            //            }
                        
            //            strDataValue += kv.Value.Value.ToString();
            //            strDataValue += "\t";
            //        }
            //        if (i == 0)
            //        {
            //            Console.WriteLine(strDataName);
            //        }
                    
            //        Console.WriteLine(strDataValue);
            //    }
            //}

            string output = GetOutputStr();
            Console.WriteLine(output);
        }

        public string GetOutputStr()
        {
            StringBuilder sb = new StringBuilder();
            if (_dataSets == null || _dataSets.Count == 0)
            {
                sb.AppendLine("Empty data.");
                return sb.ToString();
            }

            for (int k = 0; k < 1; k++)
            {
                var dataSet = _dataSets[k];
                if (dataSet == null || dataSet.Rows == null || dataSet.Rows.Count == 0)
                {
                    sb.AppendLine("Empty data.");
                    return sb.ToString();
                }

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

                    sb.AppendFormat("{0}{1}\n", strName, strValue);
                }
            }

            if (_dataSets.Count > 1)
            {
                for (int k = 1; k < _dataSets.Count; k++)
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
                            sb.AppendLine(strDataName);
                        }

                        sb.AppendLine(strDataValue);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
