using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Common
{
    public class Util
    {
        private static Hashtable mBuTable = null;
        class CaseIComparer : IEqualityComparer
        {
            public CaseInsensitiveComparer myComparer;
            public CaseIComparer()
            {
                myComparer = CaseInsensitiveComparer.DefaultInvariant;
            }
            public new bool Equals(object x, object y)
            {
                if (myComparer.Compare(x, y) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public int GetHashCode(object obj)
            {
                return obj.ToString().ToLower().GetHashCode();
            }
        }

        static Util()
        {
            mBuTable = new Hashtable(new CaseIComparer());
            mBuTable.Add("B13", "PCBA-South Campus");
            mBuTable.Add("B6", "PCBA-South Campus");
            //mBuTable.Add("PCBA Flex-CS", "PCBA-South Campus");
            //mBuTable.Add("PCBA Flex-CS-HW", "PCBA-South Campus");
            mBuTable.Add("PCBA HW", "PCBA-South Campus");
            mBuTable.Add("PCBA South Campus", "PCBA-South Campus");
            mBuTable.Add("PCBA-B13", "PCBA-South Campus");
            //mBuTable.Add("PCBA-Flex-CS", "PCBA-South Campus");
            mBuTable.Add("PCBA-HW", "PCBA-South Campus");
            mBuTable.Add("PCBA-South Campus", "PCBA-South Campus");

            mBuTable.Add("B11", "PCBA-B11");
            mBuTable.Add("PCBA B11", "PCBA-B11");

            mBuTable.Add("B15", "Mech-PCBA");
            mBuTable.Add("Mech B15 (ESD 车间）", "Mech-PCBA");
            mBuTable.Add("Mech B15 (非ESD 车间）", "Mech-PCBA");
            mBuTable.Add("Mech B17（ESD车间）", "Mech-PCBA");
            mBuTable.Add("Mech B17（非ESD车间）", "Mech-PCBA");
            mBuTable.Add("Mech B6", "Mech-PCBA");
            mBuTable.Add("Mech FMA", "Mech-PCBA");
            mBuTable.Add("Mech Mech-FMA", "Mech-PCBA");
            mBuTable.Add("Mech PCBA", "Mech-PCBA");
            mBuTable.Add("Mech-B17", "Mech-PCBA");
            mBuTable.Add("Mech-FMA", "Mech-PCBA");
            mBuTable.Add("Mech-PCBA", "Mech-PCBA");
            mBuTable.Add("PCBA B15", "Mech-PCBA");
            mBuTable.Add("PCBA-B15", "Mech-PCBA");
            mBuTable.Add("PCBA-B16", "Mech-PCBA");
            mBuTable.Add("PCBA-B17", "Mech-PCBA");

            mBuTable.Add("B7", "Mechanical");
            mBuTable.Add("Mech", "Mechanical");
            mBuTable.Add("Mech B10  (ESD 车间）", "Mechanical");
            mBuTable.Add("Mech B7（ESD车间）", "Mechanical");
            mBuTable.Add("Mech B7（非ESD车间）", "Mechanical");
            mBuTable.Add("Mech B9", "Mechanical");
            mBuTable.Add("Mechanica", "Mechanical");
            mBuTable.Add("MechB7", "Mechanical");
            mBuTable.Add("Mech-B7", "Mechanical");

            mBuTable.Add("Multek Corporate", "Corporate");
            mBuTable.Add("Multek B5", "B5");
            mBuTable.Add("Multek-B5", "B5");
            mBuTable.Add("Multek B3", "B3");
            mBuTable.Add("Multek B2F", "B2F");
            mBuTable.Add("Multek B1", "B1");
            mBuTable.Add("Multek-B1", "B1");
        }

        //进行事业部规范化
        public static string NormalBU(string bu)
        {
            if (mBuTable.Contains(bu))
            {
                return mBuTable[bu] as string;
            }
            return bu;
        }
        
    }
}
