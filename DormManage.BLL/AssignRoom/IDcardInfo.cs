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

        private byte[] _PIC_Byte;    //��Ƭ������
        private Image _PIC_Image;   //��Ƭ


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
                    _Sex = "��";
                }
                else
                {
                    _Sex = "Ů";
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
                    case "01": _Nation = "��"; break;
                    case "02": _Nation = "�ɹ�"; break;
                    case "03": _Nation = "��"; break;
                    case "04": _Nation = "��"; break;
                    case "05": _Nation = "ά���"; break;
                    case "06": _Nation = "��"; break;
                    case "07": _Nation = "��"; break;
                    case "08": _Nation = "׳"; break;
                    case "09": _Nation = "����"; break;
                    case "10": _Nation = "����"; break;
                    case "11": _Nation = "��"; break;
                    case "12": _Nation = "��"; break;
                    case "13": _Nation = "��"; break;
                    case "14": _Nation = "��"; break;
                    case "15": _Nation = "����"; break;
                    case "16": _Nation = "����"; break;
                    case "17": _Nation = "������"; break;
                    case "18": _Nation = "��"; break;
                    case "19": _Nation = "��"; break;
                    case "20": _Nation = "����"; break;
                    case "21": _Nation = "��"; break;
                    case "22": _Nation = "�"; break;
                    case "23": _Nation = "��ɽ"; break;
                    case "24": _Nation = "����"; break;
                    case "25": _Nation = "ˮ"; break;
                    case "26": _Nation = "����"; break;
                    case "27": _Nation = "����"; break;
                    case "28": _Nation = "����"; break;
                    case "29": _Nation = "�¶�����"; break;
                    case "30": _Nation = "��"; break;
                    case "31": _Nation = "�ﺲ��"; break;
                    case "32": _Nation = "����"; break;
                    case "33": _Nation = "Ǽ"; break;
                    case "34": _Nation = "����"; break;
                    case "35": _Nation = "����"; break;
                    case "36": _Nation = "ë��"; break;
                    case "37": _Nation = "����"; break;
                    case "38": _Nation = "����"; break;
                    case "39": _Nation = "����"; break;
                    case "40": _Nation = "����"; break;
                    case "41": _Nation = "������"; break;
                    case "42": _Nation = "ŭ"; break;
                    case "43": _Nation = "���α��"; break;
                    case "44": _Nation = "����˹"; break;
                    case "45": _Nation = "���¿�"; break;
                    case "46": _Nation = "�°�"; break;
                    case "47": _Nation = "����"; break;
                    case "48": _Nation = "ԣ��"; break;
                    case "49": _Nation = "��"; break;
                    case "50": _Nation = "������"; break;
                    case "51": _Nation = "����"; break;
                    case "52": _Nation = "���״�"; break;
                    case "53": _Nation = "����"; break;
                    case "54": _Nation = "�Ű�"; break;
                    case "55": _Nation = "���"; break;
                    case "56": _Nation = "��ŵ"; break;
                    case "57": _Nation = "����"; break;
                    case "98": _Nation = "������뼮"; break;
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
        //�䷢֤������
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
        //֤����Ч����
        public string EffectiveDate
        {
            get
            {
                return _EffectiveDate;
            }
            set
            {
                _EffectiveDate = Convert.ToString(value.Substring(0, 4) + "��" + value.Substring(4, 2) + "��" + value.Substring(6, 2) + "��"); ;
            }
        }
        //֤��ʧЧ����
        public string ExpiredDate
        {
            get
            {
                return _ExpiredDate;
            }
            set
            {
                if (value.Trim() != "����")
                {
                    _ExpiredDate = Convert.ToString(value.Substring(0, 4) + "��" + value.Substring(4, 2) + "��" + value.Substring(6, 2) + "��");
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
