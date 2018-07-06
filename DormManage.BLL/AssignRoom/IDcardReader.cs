using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace RecruitmentSystem.IDcardReader
{
    public static class IDcardReader
    {
        //���ȣ�����ͨ�ýӿ�
        [DllImport("sdtapi.dll")]
        public static extern int SDT_OpenPort(int iPortID);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ClosePort(int iPortID);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_PowerManagerBegin(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_AddSAMUser(int iPortID, string pcUserName, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SAMLogin(int iPortID, string pcUserName, string pcPasswd, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SAMLogout(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_UserManagerOK(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ChangeOwnPwd(int iPortID, string pcOldPasswd, string pcNewPasswd, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ChangeOtherPwd(int iPortID, string pcUserName, string pcNewPasswd, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_DeleteSAMUser(int iPortID, string pcUserName, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_StartFindIDCard(int iPortID, ref int pucIIN, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SelectIDCard(int iPortID, ref int pucSN, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadBaseMsg(int iPortID, string pucCHMsg, ref int puiCHMsgLen, string pucPHMsg, ref int puiPHMsgLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadBaseMsgToFile(int iPortID, string fileName1, ref int puiCHMsgLen, string fileName2, ref int puiPHMsgLen, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_WriteAppMsg(int iPortID, ref byte pucSendData, int uiSendLen, ref byte pucRecvData, ref int puiRecvLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_WriteAppMsgOK(int iPortID, ref byte pucData, int uiLen, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_CancelWriteAppMsg(int iPortID, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadNewAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadAllAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_UsableAppMsg(int iPortID, ref byte ucByte, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_GetUnlockMsg(int iPortID, ref byte strMsg, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_GetSAMID(int iPortID, ref byte StrSAMID, int iIfOpen);

        [DllImport("sdtapi.dll")]
        public static extern int SDT_SetMaxRFByte(int iPortID, byte ucByte, int iIfOpen);
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ResetSAM(int iPortID, int iIfOpen);

        [DllImport("WltRS.dll")]
        public static extern int GetBmp(string file_name, int intf);

        private static int EdziIfOpen = 1;                //�Զ����ش���
        static int EdziPortID;

        public static IDcardInfo GetIDcardInfo()
        {
            bool bUsbPort = false;
            int intOpenPortRtn = 0;
            int rtnTemp = 0;
            int pucIIN = 0;
            int pucSN = 0;
            int puiCHMsgLen = 0;
            int puiPHMsgLen = 0;

            //���usb�ڵĻ������ӣ������ȼ��usb
            for (int iPort = 1001; iPort <= 1016; iPort++)
            {
                intOpenPortRtn = SDT_OpenPort(iPort);
                if (intOpenPortRtn == 144)
                {
                    EdziPortID = iPort;
                    bUsbPort = true;
                    break;
                }
            }
            //��⴮�ڵĻ�������
            if (!bUsbPort)
            {
                for (int iPort = 1; iPort <= 2; iPort++)
                {
                    intOpenPortRtn = SDT_OpenPort(iPort);
                    if (intOpenPortRtn == 144)
                    {
                        EdziPortID = iPort;
                        bUsbPort = false;
                        break;
                    }
                }
            }
            if (intOpenPortRtn != 144)
            {
                throw new Exception("�˿ڴ�ʧ�ܣ�������Ӧ�Ķ˿ڻ����������Ӷ�������");
            }
            //��������������һ�β����ٺķѼ��˿ڵļ��Ĺ��̣������԰� EdziPortID �������������Ա�����ע����У�Ҳ���Ա����������ļ��У��ҾͲ���д�ˣ����ǣ�
            //��Ҫ���ǻ������Ӷ˿ڱ��û��ı�����Ŷ

            //�����ҿ�
            rtnTemp = SDT_StartFindIDCard(EdziPortID, ref pucIIN, EdziIfOpen);
            if (rtnTemp != 159)
            {
                rtnTemp = SDT_StartFindIDCard(EdziPortID, ref pucIIN, EdziIfOpen);   //���ҿ�
                if (rtnTemp != 159)
                {
                    rtnTemp = SDT_ClosePort(EdziPortID);
                    //MessageBox.Show("δ�ſ����߿�δ�źã������·ſ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            //ѡ��
            rtnTemp = SDT_SelectIDCard(EdziPortID, ref pucSN, EdziIfOpen);
            if (rtnTemp != 144)
            {
                rtnTemp = SDT_SelectIDCard(EdziPortID, ref pucSN, EdziIfOpen);   //��ѡ��
                if (rtnTemp != 144)
                {
                    rtnTemp = SDT_ClosePort(EdziPortID);
                    //MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            //ע�⣬������û�������Ӧ�ó���ǰĿ¼�Ķ�дȨ��
            FileInfo objFile = new FileInfo("wz.txt");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            objFile = new FileInfo("zp.bmp");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }
            objFile = new FileInfo("zp.wlt");
            if (objFile.Exists)
            {
                objFile.Attributes = FileAttributes.Normal;
                objFile.Delete();
            }

            rtnTemp = SDT_ReadBaseMsgToFile(EdziPortID, "wz.txt", ref puiCHMsgLen, "zp.wlt", ref puiPHMsgLen, EdziIfOpen);
            if (rtnTemp != 144)
            {
                rtnTemp = SDT_ClosePort(EdziPortID);
                //MessageBox.Show("����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (bUsbPort)
                rtnTemp = GetBmp("zp.wlt", 2);
            else
                rtnTemp = GetBmp("zp.wlt", 1);
            switch (rtnTemp)
            {
                case 0:
                    MessageBox.Show("����sdtapi.dll����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 1:    //����
                    break;
                case -1:
                    MessageBox.Show("��Ƭ�������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -2:
                    MessageBox.Show("wlt�ļ���׺����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -3:
                    MessageBox.Show("wlt�ļ��򿪴���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -4:
                    MessageBox.Show("wlt�ļ���ʽ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -5:
                    MessageBox.Show("���δ��Ȩ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case -6:
                    MessageBox.Show("�豸���Ӵ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            //�رն˿�
            rtnTemp = SDT_ClosePort(EdziPortID);
            FileInfo f = new FileInfo("wz.txt");
            FileStream fs = f.OpenRead();
            byte[] bt = new byte[fs.Length];
            fs.Read(bt, 0, (int)fs.Length);
            fs.Close();

            string str = System.Text.UnicodeEncoding.Unicode.GetString(bt);

            IDcardInfo IDcard = new IDcardInfo();
            IDcard.Name = System.Text.UnicodeEncoding.Unicode.GetString(bt, 0, 30).Trim();
            IDcard.Sex = System.Text.UnicodeEncoding.Unicode.GetString(bt, 30, 2).Trim();
            IDcard.Nation = System.Text.UnicodeEncoding.Unicode.GetString(bt, 32, 4).Trim();
            IDcard.Birth = System.Text.UnicodeEncoding.Unicode.GetString(bt, 36, 16).Trim();
            IDcard.Address = System.Text.UnicodeEncoding.Unicode.GetString(bt, 52, 70).Trim();
            IDcard.IDcardNumber = System.Text.UnicodeEncoding.Unicode.GetString(bt, 122, 36).Trim();
            IDcard.Authorities = System.Text.UnicodeEncoding.Unicode.GetString(bt, 158, 30).Trim();
            IDcard.EffectiveDate = System.Text.UnicodeEncoding.Unicode.GetString(bt, 188, bt.GetLength(0) - 188).Trim();
            IDcard.ExpiredDate = System.Text.UnicodeEncoding.Unicode.GetString(bt, 188, bt.GetLength(0) - 188).Trim().Substring(8);

            objFile = new FileInfo("zp.bmp");
            if (objFile.Exists)
            {
                FileStream fss = new FileStream("zp.bmp", FileMode.Open);
                byte[] imgbyte = new byte[(int)objFile.Length];
                fss.Read(imgbyte, 0, (int)objFile.Length);
                fss.Close();
                MemoryStream ms = new MemoryStream(imgbyte);
                Image img = Image.FromStream(ms);
                IDcard.Photo = (Image)img.Clone();
                IDcard.PIC_Byte = imgbyte;
                img.Dispose();
                ms.Dispose();
            }
            return IDcard;
        }
    }
}
