﻿using log4net;
using Model;
using Model.config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Config
{
    public class FunctionConfig
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string FunctionFileName = "function.json";
        private const string FieldFileName = "fieldtype.json";
        private Dictionary<int, FunctionItem> functionItemDict = new Dictionary<int, FunctionItem>();

        private Dictionary<int, FunctionConfigItem> functionDict = new Dictionary<int, FunctionConfigItem>();
        private Dictionary<string, FieldConfigItem> fieldDict = new Dictionary<string, FieldConfigItem>();
        private readonly Regex strReg = new Regex(@"C\d+");
        private readonly Regex intReg = new Regex(@"N\d+");
        private readonly Regex floatReg = new Regex(@"N\d+.\d+");

        public FunctionConfig()
        {
            Init();
        }

        public int Init()
        {
            InitFunction();
            InitFieldType();
            BuildFunctionItemDict();

            return 0;
        }

        public FunctionItem GetFunctionItem(FunctionCode code)
        {
            return functionItemDict[(int)code];
        }

        //public FunctionConfigItem GetFunctionItem(int code) 
        //{
        //    return functionDict[code];
        //}

        //public FieldConfigItem GetFieldItem(string name)
        //{
        //    return fieldDict[name];
        //}
        #region private method

        private int InitFunction()
        {
            string functionFilePath = RuntimeEnv.Instance.GetConfigFile(FunctionFileName);
            string functionContent = FileUtil.ReadFile(functionFilePath);
            var functions = JsonConvert.DeserializeObject<List<FunctionConfigItem>>(functionContent);
            foreach (var function in functions)
            {
                if (!functionDict.ContainsKey(function.Code))
                {
                    functionDict.Add(function.Code, function);
                }
            }

            return 0;
        }

        private int InitFieldType()
        {
            string fieldFilePath = RuntimeEnv.Instance.GetConfigFile(FieldFileName);
            string fieldContent = FileUtil.ReadFile(fieldFilePath);
            var fieldItems = JsonConvert.DeserializeObject<List<FieldConfigItem>>(fieldContent);
            foreach (var field in fieldItems)
            {
                if (!fieldDict.ContainsKey(field.Name))
                {
                    fieldDict.Add(field.Name, field);
                }
            }

            return 0;
        }

        private int BuildFunctionItemDict()
        {
            foreach (var kv in functionDict)
            {
                int code = kv.Key;
                var function = kv.Value;

                List<FieldItem> requestFields = new List<FieldItem>();
                if (function.Param != null)
                {
                    foreach (var paramitem in function.Param)
                    {
                        FieldItem fieldItem = new FieldItem
                        {
                            Name = paramitem.Name,
                            Require = (paramitem.Require == "N") ? FieldRequireType.No : FieldRequireType.Yes
                        };

                        var fieldType = paramitem.Type;
                        SetFieldAttribute(paramitem.Type, ref fieldItem);

                        requestFields.Add(fieldItem);
                    }
                }


                List<FieldItem> responseField = new List<FieldItem>();
                if (function.Response != null)
                {
                    foreach (var item in function.Response)
                    {
                        var fieldItem = new FieldItem
                        {
                            Name = item.Name
                        };

                        var fieldType = item.Type;
                        SetFieldAttribute(item.Type, ref fieldItem);

                        responseField.Add(fieldItem);
                    }
                }

                FunctionItem functionItem = new FunctionItem 
                {
                    Code = code,
                    RequestFields = requestFields,
                    ResponseFields = responseField
                };

                if (!functionItemDict.ContainsKey(code))
                {
                    functionItemDict.Add(code, functionItem);
                }
            }

            return 0;
        }

        private void SetFieldAttribute(string fieldType, ref FieldItem fieldItem)
        {
            if (!fieldDict.ContainsKey(fieldType))
            {
                logger.Info("Cannot read the FieldType in FunctionConfig.SetFieldAttribute().");
                return;
            }

            var fieldConfigItem = fieldDict[fieldType];
            fieldItem.Width = fieldConfigItem.Width;
            fieldItem.Scale = fieldConfigItem.Scale;

            if (strReg.IsMatch(fieldType))
            {
                fieldItem.Type = PackFieldType.StringType;
            }
            else if (intReg.IsMatch(fieldType))
            {
                fieldItem.Type = PackFieldType.IntType;
            }
            else if (floatReg.IsMatch(fieldType))
            {
                fieldItem.Type = PackFieldType.FloatType;
            }
            else
            {
                fieldItem.Type = PackFieldType.BinaryType;
            }
        }
        #endregion
    }
}
