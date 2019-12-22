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
    /// Ng��¼��
    /// </summary>	
    public partial class FrmEditNg��¼�� : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private Ng��¼��Info tempInfo = new Ng��¼��Info();
    	
        public FrmEditNg��¼��()
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
                Ng��¼��Info info = BLLFactory<Ng��¼��>.Instance.FindByID(ID);
                if (info != null)
                {
                	tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
                	txtNg����ʱ��.SetDateTime(info.Ng����ʱ��);	
                      txt�������κ�.Text = info.�������κ�;
                       txt����guid.Text = info.����guid;
                       txt�ξ�guid.Text = info.�ξ�guid;
                       txt�ξ�rfid.Text = info.�ξ�rfid;
                   	txt�ξ߿׺�.Value = info.�ξ߿׺�;
                   	txt�豸id.Value = info.�豸id;
                       txt��λ��.Text = info.��λ��;
    
                } 
                #endregion
                //this.btnOK.Enabled = HasFunction("Ng��¼��/Edit");             
            }
            else
            {
        
                //this.btnOK.Enabled = HasFunction("Ng��¼��/Add");  
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
            //this.txtNg����ʱ��.Tag = "Ng����ʱ��";
            //this.txt�������κ�.Tag = "�������κ�";
            //this.txt����guid.Tag = "����guid";
            //this.txt�ξ�guid.Tag = "�ξ�guid";
            //this.txt�ξ�rfid.Tag = "�ξ�rfid";
            //this.txt�ξ߿׺�.Tag = "�ξ߿׺�";
            //this.txt�豸id.Tag = "�豸id";
            //this.txt��λ��.Tag = "��λ��";
            #endregion
			
            //��ȡ�б�Ȩ�޵��б�
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Ng��¼��Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        /// <summary>
        /// �鿴�༭������Ϣ
        /// </summary>
        //private void SetAttachInfo(Ng��¼��Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = "Ng��¼��";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, info.Ng��¼id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new Ng��¼��Info();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(Ng��¼��Info info)
        {
            info.Ng����ʱ�� = txtNg����ʱ��.DateTime;
               info.�������κ� = txt�������κ�.Text;
                info.����guid = txt����guid.Text;
                info.�ξ�guid = txt�ξ�guid.Text;
                info.�ξ�rfid = txt�ξ�rfid.Text;
                info.�ξ߿׺� = Convert.ToInt32(txt�ξ߿׺�.Value);
                info.�豸id = Convert.ToInt32(txt�豸id.Value);
                info.��λ�� = txt��λ��.Text;
            }
         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            Ng��¼��Info info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������

                bool succeed = BLLFactory<Ng��¼��>.Instance.Insert(info);
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

            Ng��¼��Info info = BLLFactory<Ng��¼��>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<Ng��¼��>.Instance.Update(info, info.Ng��¼id);
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
