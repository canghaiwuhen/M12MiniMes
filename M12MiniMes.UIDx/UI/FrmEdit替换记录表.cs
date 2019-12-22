using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Dictionary;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors;

using M12MiniMes.BLL;
using M12MiniMes.Entity;

namespace M12MiniMes.UI
{
    /// <summary>
    /// �滻��¼��
    /// </summary>	
    public partial class FrmEdit�滻��¼�� : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private �滻��¼��Info tempInfo = new �滻��¼��Info();
    	
        public FrmEdit�滻��¼��()
        {
            InitializeComponent();
        }
                
        /// <summary>
        /// ʵ�ֿؼ�������ĺ���
        /// </summary>
        /// <returns></returns>
        public override bool CheckInput()
        {
            bool result = true;//Ĭ���ǿ���ͨ��

            #region MyRegion
            #endregion

            return result;
        }

        /// <summary>
        /// ��ʼ�������ֵ�
        /// </summary>
        private void InitDictItem()
        {
			//��ʼ������
        }                        

        /// <summary>
        /// ������ʾ�ĺ���
        /// </summary>
        public override void DisplayData()
        {
            InitDictItem();//�����ֵ���أ����ã�

            if (!string.IsNullOrEmpty(ID))
            {
                #region ��ʾ��Ϣ
                �滻��¼��Info info = BLLFactory<�滻��¼��>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
                	txt�滻ʱ��.SetDateTime(info.�滻ʱ��);	
                      txt�������κ�.Text = info.�������κ�;
                       txt����guid.Text = info.����guid;
                       txt�滻ǰ�ξ�guid.Text = info.�滻ǰ�ξ�guid;
                       txt�滻ǰ�ξ�rfid.Text = info.�滻ǰ�ξ�rfid;
                   	txt�滻ǰ�ξ߿׺�.Value = info.�滻ǰ�ξ߿׺�;
                       txt�滻���ξ�guid.Text = info.�滻���ξ�guid;
                       txt�滻���ξ�rfid.Text = info.�滻���ξ�rfid;
                   	txt�滻���ξ߿׺�.Value = info.�滻���ξ߿׺�;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("�滻��¼��/Edit");             
            }
            else
            {
         
                //this.btnOK.Enabled = HasFunction("�滻��¼��/Add");  
            }
            
            //tempInfo�ڶ��������Ϊָ�������½�����ȫ�µĶ��󣬵���һЩ��ʼ����GUID���ڸ����ϴ�
            //SetAttachInfo(tempInfo);
			
            //SetPermit(); //Ĭ�ϲ�ʹ���ֶ�Ȩ��
        }

        /// <summary>
        /// ���ÿؼ��ֶε�Ȩ����ʾ��������(Ĭ�ϲ�ʹ���ֶ�Ȩ��)
        /// </summary>
        private void SetPermit()
        {
            #region ���ÿؼ����ֶεĶ�Ӧ��ϵ
            //this.txt�滻ʱ��.Tag = "�滻ʱ��";
            //this.txt�������κ�.Tag = "�������κ�";
            //this.txt����guid.Tag = "����guid";
            //this.txt�滻ǰ�ξ�guid.Tag = "�滻ǰ�ξ�guid";
            //this.txt�滻ǰ�ξ�rfid.Tag = "�滻ǰ�ξ�rfid";
            //this.txt�滻ǰ�ξ߿׺�.Tag = "�滻ǰ�ξ߿׺�";
            //this.txt�滻���ξ�guid.Tag = "�滻���ξ�guid";
            //this.txt�滻���ξ�rfid.Tag = "�滻���ξ�rfid";
            //this.txt�滻���ξ߿׺�.Tag = "�滻���ξ߿׺�";
            #endregion
			
            //��ȡ�б�Ȩ�޵��б�
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(�滻��¼��Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// �鿴�༭������Ϣ
        /// </summary>
        //private void SetAttachInfo(�滻��¼��Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "�滻��¼��";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.�滻��¼id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new �滻��¼��Info();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(�滻��¼��Info info)
        {
            info.�滻ʱ�� = txt�滻ʱ��.DateTime;
               info.�������κ� = txt�������κ�.Text;
                info.����guid = txt����guid.Text;
                info.�滻ǰ�ξ�guid = txt�滻ǰ�ξ�guid.Text;
                info.�滻ǰ�ξ�rfid = txt�滻ǰ�ξ�rfid.Text;
                info.�滻ǰ�ξ߿׺� = Convert.ToInt32(txt�滻ǰ�ξ߿׺�.Value);
                info.�滻���ξ�guid = txt�滻���ξ�guid.Text;
                info.�滻���ξ�rfid = txt�滻���ξ�rfid.Text;
                info.�滻���ξ߿׺� = Convert.ToInt32(txt�滻���ξ߿׺�.Value);
            }
         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            �滻��¼��Info info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������

                bool succeed = BLLFactory<�滻��¼��>.Instance.Insert(info);
                if (succeed)
                {
                    //�����������������

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogTextHelper.Error(ex);
                MessageDxUtil.ShowError(ex.Message);
            }
            return false;
        }                 

        /// <summary>
        /// �༭״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveUpdated()
        {

            �滻��¼��Info info = BLLFactory<�滻��¼��>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<�滻��¼��>.Instance.Update(info, info.�滻��¼id);
                    if (succeed)
                    {
                        //�����������������
                       
                        return true;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
           return false;
        }
    }
}
