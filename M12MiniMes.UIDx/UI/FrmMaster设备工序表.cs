using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

using WHC.Pager.Entity;
using WHC.Dictionary;
using WHC.Framework.BaseUI;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;

using M12MiniMes.BLL;
using M12MiniMes.Entity;

namespace M12MiniMes.UI
{
    /// <summary>
    /// �豸��    
    /// </summary>	
    public partial class FrmMaster�豸����� : BaseDock
    {
        public FrmMaster�豸�����()
        {
            InitializeComponent();

            InitDictItem();

			//�����б���Ϣ
            this.winGridViewPager1.OnPageChanged += new EventHandler(winGridViewPager1_OnPageChanged);
            this.winGridViewPager1.OnStartExport += new EventHandler(winGridViewPager1_OnStartExport);
            this.winGridViewPager1.OnRefresh += new EventHandler(winGridViewPager1_OnRefresh);

             this.winGridViewPager1.OnEditSelected += new EventHandler(winGridViewPager1_OnEditSelected);
             this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);

            this.winGridViewPager1.OnDeleteSelected += new EventHandler(winGridViewPager1_OnDeleteSelected);
            this.winGridViewPager1.AppendedMenu = this.contextMenuStrip1;
            this.winGridViewPager1.ShowLineNumber = true;
            this.winGridViewPager1.BestFitColumnWith = false;//�Ƿ�����Ϊ�Զ�������ȣ�falseΪ������
			this.winGridViewPager1.gridView1.DataSourceChanged +=new EventHandler(gridView1_DataSourceChanged);
            this.winGridViewPager1.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView1_CustomColumnDisplayText);
            this.winGridViewPager1.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView1_RowCellStyle);
            this.winGridViewPager1.gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;

			//�ӱ���ϸ��Ϣ
            this.winGridView2.OnRefresh += new EventHandler(winGridView2_OnRefresh);
            this.winGridView2.ShowLineNumber = true;
            this.winGridView2.BestFitColumnWith = false;;//�Ƿ�����Ϊ�Զ�������ȣ�falseΪ������
            this.winGridView2.gridView1.DataSourceChanged += new EventHandler(gridView2_DataSourceChanged);
            this.winGridView2.gridView1.CustomColumnDisplayText +=new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(gridView2_CustomColumnDisplayText);
			
            this.chkTableDirection.CheckedChanged += new System.EventHandler(this.chkTableDirection_CheckedChanged);
			this.btnAddNew.Visible = true ; //�����Ҫ��ʾ��������Ϊtrue���������ע�ʹ���

            //�����س������в�ѯ
            foreach (Control control in this.layoutControl1.Controls)
            {
                control.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SearchControl_KeyUp);
            }
        }
		
        /// <summary>
        /// ��д��ʼ�������ʵ�֣���������ˢ��
        /// </summary>
        public override void  FormOnLoad()
        {   
            BindData();
        } 
        
        /// <summary>
        /// ��ʼ���ֵ��б�����
        /// </summary>
        private void InitDictItem()
        {
			//��ʼ������
			//this.txtCategory.BindDictItems("��������");
        }
		
        #region �����б���Ϣ
        /// <summary>
        /// �������
        /// </summary>		
        private void AddData()
        {
            FrmEdit�豸����� dlg = new FrmEdit�豸�����();
            dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
            dlg.InitFunction(LoginUserInfo, FunctionDict);//���Ӵ��帳ֵ�û�Ȩ����Ϣ
            
            if (DialogResult.OK == dlg.ShowDialog())
            {
                BindData();
            }
        }
        /// <summary>
        /// �༭�б�����
        /// </summary>
        private void EditData()
        {
            string ID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("�豸id");
            List<string> IDList = new List<string>();
            for (int i = 0; i < this.winGridViewPager1.gridView1.RowCount; i++)
            {
                string strTemp = this.winGridViewPager1.GridView1.GetRowCellDisplayText(i, "�豸id");
                IDList.Add(strTemp);
            }

            if (!string.IsNullOrEmpty(ID))
            {
                FrmEdit�豸����� dlg = new FrmEdit�豸�����();
                dlg.ID = ID;
                dlg.IDList = IDList;
                dlg.OnDataSaved += new EventHandler(dlg_OnDataSaved);
                dlg.InitFunction(LoginUserInfo, FunctionDict);//���Ӵ��帳ֵ�û�Ȩ����Ϣ
                
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    BindData();
                }
            }			
		}
		
        /// <summary>
        /// ɾ��ѡ���б�����
        /// </summary>		
        private void DeleteData()
        {
            if (MessageDxUtil.ShowYesNoAndTips("��ȷ��ɾ��ѡ���ļ�¼ô��") == DialogResult.No)
            {
                return;
            }

            int[] rowSelected = this.winGridViewPager1.GridView1.GetSelectedRows();
            foreach (int iRow in rowSelected)
            {
                string ID = this.winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "�豸id");
                BLLFactory<�豸��>.Instance.Delete(ID);
            }
             
            BindData();			
		}		

        void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "OrderStatus")
            //{
            //    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
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
            //else if (fieldName == "Operator" || fieldName == "Editor" || fieldName == "Creator")
            //{
            //    if (e.Value != null)
            //    {
            //        e.DisplayText = SecurityHelper.GetFullNameByID(e.Value.ToString());
            //    }
            //}
            //else if (columnName == "Age")
            //{
            //    e.DisplayText = string.Format("{0}��", e.Value);
            //}
            //else if (columnName == "ReceivedMoney")
            //{
            //    if (e.Value != null)
            //    {
            //        e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
            //    }
            //}
        }
        
        /// <summary>
        /// �����ݺ󣬷�����еĿ��
        /// </summary>
        private void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridViewPager1.gridView1.Columns.Count > 0 && this.winGridViewPager1.gridView1.RowCount > 0)
            {
                //ͳһ����100���
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridViewPager1.gridView1.Columns)
                {
                    column.Width = 100;
                }

				GridView gridView = this.winGridViewPager1.gridView1;
                if (gridView != null)
                {
					//�豸id,�豸����,λ�����,����״̬,����״̬
					//gridView.SetGridColumWidth("Note", 200);
                }
            }
        }

        /// <summary>
        /// �����б�ѡ������ƶ��н���Ĵ����¼�����
        /// </summary>
        void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //��ȡ�����ͷID
            string headerID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("�豸id");
            BindDetail(headerID);
        }

        /// <summary>
        /// ��ҳ�ؼ�ˢ�²���
        /// </summary>
        private void winGridViewPager1_OnRefresh(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ�ɾ������
        /// </summary>
        private void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
        {
			DeleteData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ��༭�����
        /// </summary>
        private void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
        {
						EditData();
			        }        
        
        void dlg_OnDataSaved(object sender, EventArgs e)
        {
            BindData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ���������
        /// </summary>        
        private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
        {
            AddData();
        }
        
        /// <summary>
        /// ��ҳ�ؼ�ȫ����������ǰ�Ĳ���
        /// </summary> 
        private void winGridViewPager1_OnStartExport(object sender, EventArgs e)
        {
            string where = GetConditionSql();
            this.winGridViewPager1.AllToExport = BLLFactory<�豸��>.Instance.FindToDataTable(where);
         }

        /// <summary>
        /// ��ҳ�ؼ���ҳ�Ĳ���
        /// </summary> 
        private void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
        {
            BindData();
        }
 
        #endregion

        #region �ӱ���ϸ�б�

        void winGridView2_OnRefresh(object sender, EventArgs e)
        {
            //��ȡ���ѱ�ͷID
            string headerID = this.winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("�豸id");
            BindDetail(headerID);
        }

        /// <summary>
        /// ��������ϸ�б�����
        /// </summary>
        private void BindDetail(string headerID)
        {
        	//entity
            this.winGridView2.DisplayColumns = "����id,��������,�豸id,�豸����";
            this.winGridView2.ColumnNameAlias = BLLFactory<�豸�����>.Instance.GetColumnNameAlias();//�ֶ�����ʾ����ת��

            #region ��ӱ�������

            //this.winGridView2.AddColumnAlias("����id", "����ID");
            //this.winGridView2.AddColumnAlias("��������", "��������");
            //this.winGridView2.AddColumnAlias("�豸id", "�豸ID");
            //this.winGridView2.AddColumnAlias("�豸����", "�豸����");

            #endregion

            string where = string.Format("�豸ID ='{0}'", headerID);
	            List<�豸�����Info> list = BLLFactory<�豸�����>.Instance.Find(where);
            this.winGridView2.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<�����Info>(list);
                this.winGridView2.PrintTitle = "�������";
 

             CreateSummary();// ��ϸ���ӻ�����Ϣ
        }

        /// <summary>
        /// ��ϸ���ӻ�����Ϣ
        /// </summary>
        private void CreateSummary()
        {
            DevExpress.XtraGrid.Views.Grid.GridView gridView1 = this.winGridView2.gridView1;
            if (gridView1 != null && gridView1.Columns.Count > 0)
            {
                gridView1.GroupSummary.Clear();

                gridView1.OptionsView.ColumnAutoWidth = false;
                gridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
                gridView1.OptionsView.ShowFooter = true;
                gridView1.OptionsView.ShowGroupedColumns = true;
                gridView1.OptionsView.ShowGroupPanel = false;

                DevExpress.XtraGrid.Columns.GridColumn ����idColumn = gridView1.Columns["����id"];
                ����idColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "����id", "����ID��{0}")});
                DevExpress.XtraGrid.Columns.GridColumn ��������Column = gridView1.Columns["��������"];
                ��������Column.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "��������", "�������ƣ�{0}")});
                DevExpress.XtraGrid.Columns.GridColumn �豸idColumn = gridView1.Columns["�豸id"];
                �豸idColumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "�豸id", "�豸ID��{0}")});
                DevExpress.XtraGrid.Columns.GridColumn �豸����Column = gridView1.Columns["�豸����"];
                �豸����Column.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
                    new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "�豸����", "�豸���ƣ�{0}")});

            }
        }

        /// <summary>
        /// �����ݺ󣬷�����еĿ��
        /// </summary>
        private void gridView2_DataSourceChanged(object sender, EventArgs e)
        {
            if (this.winGridView2.gridView1.Columns.Count > 0 && this.winGridView2.gridView1.RowCount > 0)
            {
                //ͳһ����100���
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in this.winGridView2.gridView1.Columns)
                {
                    column.Width = 80;
                }
				GridView gridView1 = this.winGridView2.gridView1;
                if (gridView1 != null)
                {
                //gridView1.SetGridColumWidth("����id", 120);
                //gridView1.SetGridColumWidth("��������", 120);
                //gridView1.SetGridColumWidth("�豸id", 120);
                //gridView1.SetGridColumWidth("�豸����", 120);
                gridView1.SetGridColumWidth("����id", 150);
                gridView1.SetGridColumWidth("��������", 150);
                gridView1.SetGridColumWidth("�豸id", 150);
                gridView1.SetGridColumWidth("�豸����", 150);
                }
            }
        }

        /// <summary>
        /// �����ʾ����ת��
        /// </summary>
        void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            if (fieldName == "SalePrice" || fieldName == "Price" || fieldName == "Amount")
            {
                if (e.Value != null)
                {
                    e.DisplayText = e.Value.ToString().ToDecimal().ToString("C2");
                }
            }
        }
        
        #endregion

       
        
        /// <summary>
        /// �߼���ѯ����������
        /// </summary>
        private SearchCondition advanceCondition;
        
        /// <summary>
        /// ���ݲ�ѯ���������ѯ���
        /// </summary> 
        private string GetConditionSql()
        {
            //������ڸ߼���ѯ������Ϣ����ʹ�ø߼���ѯ����������ʹ������������ѯ
            SearchCondition condition = advanceCondition;
            if (condition == null)
            {
                condition = new SearchCondition();
                condition.AddCondition("�豸����", this.txt�豸����.Text.Trim(), SqlOperator.Like);
                condition.AddNumericCondition("λ�����", this.txtλ�����1, this.txtλ�����2); //��ֵ����
                condition.AddCondition("����״̬", this.txt����״̬.Text.Trim(), SqlOperator.Like);
                condition.AddCondition("����״̬", this.txt����״̬.Text.Trim(), SqlOperator.Like);
            }
            string where = condition.BuildConditionSql().Replace("Where", "");
            return where;
        }
        
        /// <summary>
        /// ���б�����
        /// </summary>
        private void BindData()
        {
        	//entity
            this.winGridViewPager1.DisplayColumns = "�豸id,�豸����,λ�����,����״̬,����״̬";
            this.winGridViewPager1.ColumnNameAlias = BLLFactory<�豸��>.Instance.GetColumnNameAlias();//�ֶ�����ʾ����ת��

            #region ��ӱ�������

            //this.winGridViewPager1.AddColumnAlias("�豸id", "�豸ID");
            //this.winGridViewPager1.AddColumnAlias("�豸����", "�豸����");
            //this.winGridViewPager1.AddColumnAlias("λ�����", "λ�����");
            //this.winGridViewPager1.AddColumnAlias("����״̬", "����״̬");
            //this.winGridViewPager1.AddColumnAlias("����״̬", "����״̬");

            #endregion

            string where = GetConditionSql();
	            List<�豸��Info> list = BLLFactory<�豸��>.Instance.FindWithPager(where, this.winGridViewPager1.PagerInfo);
            this.winGridViewPager1.DataSource = list;//new WHC.Pager.WinControl.SortableBindingList<�豸��Info>(list);
                this.winGridViewPager1.PrintTitle = "�豸����";
         }
        
        /// <summary>
        /// ��ѯ���ݲ���
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
        	advanceCondition = null;//�������ò�ѯ������������ܻ�ʹ�ø߼���ѯ������
            BindData();
        }
        
        /// <summary>
        /// �ṩ���ؼ��س�ִ�в�ѯ�Ĳ���
        /// </summary>
        private void SearchControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }    
		     
        /// <summary>
        /// �������ݲ���
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddData();
        }  

 		 		 		 		 
        private string moduleName = "�豸��";
        /// <summary>
        /// ����Excel�Ĳ���
        /// </summary>          
        private void btnImport_Click(object sender, EventArgs e)
        {
            string templateFile = string.Format("{0}-ģ��.xls", moduleName);
            FrmImportExcelData dlg = new FrmImportExcelData();
            dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
            dlg.OnDataSave += new FrmImportExcelData.SaveDataHandler(ExcelData_OnDataSave);
            dlg.OnRefreshData += new EventHandler(ExcelData_OnRefreshData);
            dlg.ShowDialog();
        }

        void ExcelData_OnRefreshData(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// ����ֶδ��ڣ����ȡ��Ӧ��ֵ�����򷵻�Ĭ�Ͽ�
        /// </summary>
        /// <param name="row">DataRow����</param>
        /// <param name="columnName">�ֶ�����</param>
        /// <returns></returns>
        private string GetRowData(DataRow row, string columnName)
        {
            string result = "";
            if (row.Table.Columns.Contains(columnName))
            {
                result = row[columnName].ToString();
            }
            return result;
        }
        
        bool ExcelData_OnDataSave(DataRow dr)
        {
            bool success = false;
            bool converted = false;
            DateTime dtDefault = Convert.ToDateTime("1900-01-01");
            DateTime dt;
            �豸��Info info = new �豸��Info();
             info.�豸���� = GetRowData(dr, "�豸����");
              info.λ����� = GetRowData(dr, "λ�����").ToInt32();
              info.����״̬ = GetRowData(dr, "����״̬").ToBoolean();
              info.����״̬ = GetRowData(dr, "����״̬");
  
            success = BLLFactory<�豸��>.Instance.Insert(info);
             return success;
        }

        /// <summary>
        /// ����Excel�Ĳ���
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string file = FileDialogHelper.SaveExcel(string.Format("{0}.xls", moduleName));
            if (!string.IsNullOrEmpty(file))
            {
                string where = GetConditionSql();
                List<�豸��Info> list = BLLFactory<�豸��>.Instance.Find(where);
                 DataTable dtNew = DataTableHelper.CreateTable("���|int,�豸����,λ�����,����״̬,����״̬");
                DataRow dr;
                int j = 1;
                for (int i = 0; i < list.Count; i++)
                {
                    dr = dtNew.NewRow();
                    dr["���"] = j++;
                     dr["�豸����"] = list[i].�豸����;
                     dr["λ�����"] = list[i].λ�����;
                     dr["����״̬"] = list[i].����״̬;
                     dr["����״̬"] = list[i].����״̬;
                     dtNew.Rows.Add(dr);
                }

                try
                {
                    string error = "";
                    AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageDxUtil.ShowError(string.Format("����Excel���ִ���{0}", error));
                    }
                    else
                    {
                        if (MessageDxUtil.ShowYesNoAndTips("�����ɹ����Ƿ���ļ���") == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(file);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogTextHelper.Error(ex);
                    MessageDxUtil.ShowError(ex.Message);
                }
            }
         }
         
        private FrmAdvanceSearch dlg;
        private void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
            if (dlg == null)
            {
                dlg = new FrmAdvanceSearch();
                dlg.FieldTypeTable = BLLFactory<�豸��>.Instance.GetFieldTypeList();
                dlg.ColumnNameAlias = BLLFactory<�豸��>.Instance.GetColumnNameAlias();                
                 dlg.DisplayColumns = "�豸����,λ�����,����״̬,����״̬";

                #region �����б�����

                //dlg.AddColumnListItem("UserType", Portal.gc.GetDictData("��Ա����"));//�ֵ��б�
                //dlg.AddColumnListItem("Sex", "��,Ů");//�̶��б�
                //dlg.AddColumnListItem("Credit", BLLFactory<�豸��>.Instance.GetFieldList("Credit"));//��̬�б�

                #endregion

                dlg.ConditionChanged += new FrmAdvanceSearch.ConditionChangedEventHandler(dlg_ConditionChanged);
            }
            dlg.ShowDialog();
        }

        void dlg_ConditionChanged(SearchCondition condition)
        {
            advanceCondition = condition;
            BindData();
        }
		
        private void chkTableDirection_CheckedChanged(object sender, EventArgs e)
        {
            this.splitContainer1.Horizontal = !this.chkTableDirection.Checked;
        }
    }
}
