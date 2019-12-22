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
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;

using M12MiniMes.BLL;
using M12MiniMes.Entity;

namespace M12MiniMes.UI
{
    /// <summary>
    /// �豸��
    /// </summary>	
    public partial class FrmEdit�豸������ : BaseEditForm
    {
    	/// <summary>
        /// ����һ����ʱ���󣬷����ڸ��������л�ȡ���ڵ�GUID
        /// </summary>
    	private �豸��Info tempInfo = new �豸��Info();
    	
        public FrmEdit�豸������()
        {
            InitializeComponent();

            InitDetailGrid();
        }

        /// <summary>
        /// ��ʼ����ϸ���GridView������ʾ
        /// </summary>
        private void InitDetailGrid()
        {
			//������Ӧ����
            this.gridView1.Columns.Clear();
            this.gridView1.CreateColumn("ID", "���").Visible = false;//�ӱ��ID��������
            this.gridView1.CreateColumn("�豸ID", "������").Visible = false;
 			this.gridView1.CreateColumn("��������", "��������", 100).CreateTextEdit();
  			this.gridView1.CreateColumn("����ֵ", "����ֵ", 100).CreateTextEdit();
  			this.gridView1.CreateColumn("�豸id", "�豸ID", 100).CreateSpinEdit();
  			this.gridView1.CreateColumn("�豸����", "�豸����", 100).CreateTextEdit();
  
            this.gridView1.InitGridView(GridType.NewItem, false, EditorShowMode.MouseDownFocused, "");
            this.gridView1.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.CustomDrawRowIndicator += (s, e) =>
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            };
            this.gridView1.ValidateRow += delegate(object sender, ValidateRowEventArgs e)
            {
				//�����ҪУ��ǿ����룬��ô��Ӷ�Ӧ�ֶ�
                //var result = gridControl1.ValidateRowNull(e, new string[]
                //{
                //    "����id,��������,����ֵ,�豸id,�豸����"
                //});
            };
            this.gridView1.InitNewRow += (s, e) =>
            {
				//�˴����������е����ݳ�ʼ��
                gridView1.SetRowCellValue(e.RowHandle, "�豸id", Guid.NewGuid().ToString());   //��ϸ��ID
                gridView1.SetRowCellValue(e.RowHandle, "�豸ID", tempInfo.�豸id);//��ϸ������
                //gridView1.SetRowCellValue(e.RowHandle, "Apply_ID", tempInfo.Apply_ID);
                //gridView1.SetRowCellValue(e.RowHandle, "OccurTime", DateTime.Now);
            };
        }

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView gridView = this.gridView1;
            //if (e.Column.FieldName == "OrderStatus")
            //{
            //    string status = gridView.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
            //    Color color = Color.White;
            //    if (status == "�����")
            //    {
            //        e.Appearance.BackColor = Color.Red;
            //        e.Appearance.BackColor2 = Color.LightCyan;
            //    }
            //}
        }
        void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            string columnName = e.Column.FieldName;
            if (e.Column.ColumnType == typeof(DateTime))
            {
                if (e.Value != null)
                {
                    if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                    {
                        e.DisplayText = "";
                    }
                    else
                    {
                        e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                    }
                }
            }
            //else if (columnName == "DictType_ID")
            //{
            //    e.DisplayText = BLLFactory<DictType>.Instance.GetFieldValue(string.Concat(e.Value), "Name");
            //}
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

            var list = new List<�豸������Info>();
            if (!string.IsNullOrEmpty(ID))
            {
                #region ��ʾ��Ϣ
                �豸��Info info = BLLFactory<�豸��>.Instance.FindByID(ID);
                if (info != null)
                {
					DisplayInfo(info);
                    string condition = $@"�豸id = {info.�豸id}";
                    list = BLLFactory<�豸������>.Instance.Find(condition);//���������ȡ��ϸ�б��¼
                } 
                #endregion

                //this.btnOK.Enabled = HasFunction("�豸��/Edit");             
            }
            else
            {
    
                //this.btnOK.Enabled = Portal.gc.HasFunction("�豸��/Add");  
            }

            //ͳһչʾ��ϸ���ݣ�û����󶨿�����Դ
            this.gridControl1.DataSource = new BindingList<�豸������Info>(list);            
            //tempInfo�ڶ��������Ϊָ�������½�����ȫ�µĶ��󣬵���һЩ��ʼ����GUID���ڸ����ϴ�
            //SetAttachInfo(tempInfo);
			
            //SetPermit(); //Ĭ�ϲ�ʹ���ֶ�Ȩ��
        }

        private void DisplayInfo(�豸��Info info)
        {
             tempInfo = info;//���¸���ʱ����ֵ��ʹָ֮����ڵļ�¼����
                	
             txt�豸����.Text = info.�豸����;
                txtλ�����.Value = info.λ�����;
                txt����״̬.Text = info.����״̬.ToString();
                txt����״̬.Text = info.����״̬;
            }

        /// <summary>
        /// ���ÿؼ��ֶε�Ȩ����ʾ��������(Ĭ�ϲ�ʹ���ֶ�Ȩ��)
        /// </summary>
        private void SetPermit()
        {
            #region ���ÿؼ����ֶεĶ�Ӧ��ϵ
            //this.txt�豸����.Tag = "�豸����";
            //this.txtλ�����.Tag = "λ�����";
            //this.txt����״̬.Tag = "����״̬";
            //this.txt����״̬.Tag = "����״̬";
            #endregion
			
            //��ȡ�б�Ȩ�޵��б�
            //var permitDict = BLLFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(�豸��Info).FullName, LoginUserInfo.ID.ToInt32());
			//this.SetControlPermit(permitDict, this.layoutControl1);
		}

        //private void SetAttachInfo(�豸��Info info)
        //{
        //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
        //    this.attachmentGUID.userId = LoginUserInfo.Name;

        //    string name = txtName.Text;
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        string dir = string.Format("{0}", name);
        //        this.attachmentGUID.Init(dir, tempInfo.�豸id, LoginUserInfo.Name);
        //    }
        //}

        public override void ClearScreen()
        {
            this.tempInfo = new �豸��Info();
            base.ClearScreen();
        }

        /// <summary>
        /// �༭���߱���״̬��ȡֵ����
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(�豸��Info info)
        {
            info.�豸���� = txt�豸����.Text;
                info.λ����� = Convert.ToInt32(txtλ�����.Value);
                info.����״̬ = txt����״̬.Text.ToBoolean();
                info.����״̬ = txt����״̬.Text;
            }

        /// <summary>
        /// ��ȡ��ϸ�б�
        /// </summary>
        /// <returns></returns>
        private List<�豸������Info> GetDetailList()
        {
            var list = new List<�豸������Info>();
            for (int i = 0; i < this.gridView1.RowCount; i++)
            {
                var detailInfo = gridView1.GetRow(i) as �豸������Info;
                if (detailInfo != null)
                {
                    list.Add(detailInfo);
                }
            }
            return list;
        }
		         
        /// <summary>
        /// ����״̬�µ����ݱ���
        /// </summary>
        /// <returns></returns>
        public override bool SaveAddNew()
        {
            �豸��Info info = tempInfo;//����ʹ�ô��ڵľֲ���������Ϊ������Ϣ���ܱ�����ʹ��
            SetInfo(info);

            try
            {
                #region ��������

                bool succeed = BLLFactory<�豸��>.Instance.Insert(info);
                if (succeed)
                {
                    //�����������������
                    var list = GetDetailList();
                    foreach(var detailInfo in list)
                    {
                        BLLFactory<�豸������>.Instance.InsertUpdate(detailInfo, detailInfo.����id);
                    }
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

            �豸��Info info = BLLFactory<�豸��>.Instance.FindByID(ID);
            if (info != null)
            {
                SetInfo(info);

                try
                {
                    #region ��������
                    bool succeed = BLLFactory<�豸��>.Instance.Update(info, info.�豸id);
                    if (succeed)
                    {
                        //�����������������
						var list = GetDetailList();
						foreach(var detailInfo in list)
						{
							BLLFactory<�豸������>.Instance.InsertUpdate(detailInfo, detailInfo.����id);
						}                       
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
