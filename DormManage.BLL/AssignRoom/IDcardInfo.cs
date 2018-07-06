using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace RecruitmentSystem.IDcardReader
{
    public class IDcardInfo
    {
        private string _Name;
        private string _Sex;
        private string _Nation;
        private string _BirthYear;
        private string _BirthMonth;
        private string _BirthDay;
        private string _Address;
        private string _IDcardNumber;
        private string _Authorities
;
        private string _EffectiveDate;
        private string _ExpiredDate;

        private byte[] _PIC_Byte;    //照片二进制
        private Image _PIC_Image;   //照片


        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public string Sex
        {
            get
            {
                return _Sex;
            }
            set
            {
                if (value == "1")
                {
                    _Sex = "男";
                }
                else
                {
                    _Sex = "女";
                }
            }
        }
        public string Nation
        {
            get
            {
                return _Nation;
            }
            set
            {
                switch (value)
                {
                    case "01": _Nation = "汉"; break;
                    case "02": _Nation = "蒙古"; break;
                    case "03": _Nation = "回"; break;
                    case "04": _Nation = "藏"; break;
                    case "05": _Nation = "维吾尔"; break;
                    case "06": _Nation = "苗"; break;
                    case "07": _Nation = "彝"; break;
                    case "08": _Nation = "壮"; break;
                    case "09": _Nation = "布依"; break;
                    case "10": _Nation = "朝鲜"; break;
                    case "11": _Nation = "满"; break;
                    case "12": _Nation = "侗"; break;
                    case "13": _Nation = "瑶"; break;
                    case "14": _Nation = "白"; break;
                    case "15": _Nation = "土家"; break;
                    case "16": _Nation = "哈尼"; break;
                    case "17": _Nation = "哈萨克"; break;
                    case "18": _Nation = "傣"; break;
                    case "19": _Nation = "黎"; break;
                    case "20": _Nation = "傈僳"; break;
                    case "21": _Nation = "佤"; break;
                    case "22": _Nation = "畲"; break;
                    case "23": _Nation = "高山"; break;
                    case "24": _Nation = "拉祜"; break;
                    case "25": _Nation = "水"; break;
                    case "26": _Nation = "东乡"; break;
                    case "27": _Nation = "纳西"; break;
                    case "28": _Nation = "景颇"; break;
                    case "29": _Nation = "柯尔克孜"; break;
                    case "30": _Nation = "土"; break;
                    case "31": _Nation = "达翰尔"; break;
                    case "32": _Nation = "仫佬"; break;
                    case "33": _Nation = "羌"; break;
                    case "34": _Nation = "布朗"; break;
                    case "35": _Nation = "撒拉"; break;
                    case "36": _Nation = "毛南"; break;
                    case "37": _Nation = "仡佬"; break;
                    case "38": _Nation = "锡伯"; break;
                    case "39": _Nation = "阿昌"; break;
                    case "40": _Nation = "普米"; break;
                    case "41": _Nation = "塔吉克"; break;
                    case "42": _Nation = "怒"; break;
                    case "43": _Nation = "乌孜别克"; break;
                    case "44": _Nation = "俄罗斯"; break;
                    case "45": _Nation = "鄂温克"; break;
                    case "46": _Nation = "德昂"; break;
                    case "47": _Nation = "保安"; break;
                    case "48": _Nation = "裕固"; break;
                    case "49": _Nation = "京"; break;
                    case "50": _Nation = "塔塔尔"; break;
                    case "51": _Nation = "独龙"; break;
                    case "52": _Nation = "鄂伦春"; break;
                    case "53": _Nation = "赫哲"; break;
                    case "54": _Nation = "门巴"; break;
                    case "55": _Nation = "珞巴"; break;
                    case "56": _Nation = "基诺"; break;
                    case "57": _Nation = "其它"; break;
                    case "98": _Nation = "外国人入籍"; break;
                }

            }
        }
        public string Birth
        {
            set
            {
                _BirthYear = value.Substring(0, 4);
                _BirthMonth = value.Substring(4, 2);
                _BirthDay = value.Substring(6);
            }
            get
            {
                return _BirthYear + "-" + _BirthMonth + "-" + _BirthDay;
            }
        }
        public string BirthYear
        {
            get
            {
                return _BirthYear;
            }
        }
        public string BirthMonth
        {
            get
            {
                return _BirthMonth;
            }
        }
        public string BirthDay
        {
            get
            {
                return _BirthDay;
            }
        }
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
        public string IDcardNumber
        {
            get
            {
                return _IDcardNumber;
            }
            set
            {
                _IDcardNumber = value;
            }
        }
        //颁发证件机构
        public string Authorities
        {
            get
            {
                return _Authorities
;
            }
            set
            {
                _Authorities = value;
            }
        }
        //证件生效日期
        public string EffectiveDate
        {
            get
            {
                return _EffectiveDate;
            }
            set
            {
                _EffectiveDate = Convert.ToString(value.Substring(0, 4) + "年" + value.Substring(4, 2) + "月" + value.Substring(6, 2) + "日"); ;
            }
        }
        //证件失效日期
        public string ExpiredDate
        {
            get
            {
                return _ExpiredDate;
            }
            set
            {
                if (value.Trim() != "长期")
                {
                    _ExpiredDate = Convert.ToString(value.Substring(0, 4) + "年" + value.Substring(4, 2) + "月" + value.Substring(6, 2) + "日");
                }
                else
                {
                    _ExpiredDate = Convert.ToString(DateTime.MaxValue);
                }
            }
        }
        public byte[] PIC_Byte
        {
            get { return _PIC_Byte; }
            set { _PIC_Byte = value; }
        }
        public Image Photo
        {
            get { return _PIC_Image; }
            set { _PIC_Image = value; }
        }
    }
}
