﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M12MiniMes.UIStart
{
    /// <summary>
    /// 治具Item
    /// </summary>
    [Serializable]
    public class FixtureItem : IDisposable
    {
        /// <summary>
        /// 治具上RFID扫描出
        /// </summary>
        public string RFID;

        /// <summary>
        /// 治具有12个孔位，记录12个物料信息
        /// </summary>
        public BindingList<MaterialItem> MaterialItems { get; set; }

        /// <summary>
        /// 当前治具所在的设备信息，可以为null
        /// </summary>
        public MachineItem MachineItem { get; private set; }

        /// <summary>
        /// 治具GUID
        /// </summary>
        public Guid FixtureGuid { get; private set; }

        public string 治具生产批次号 { get; set; }

        /// <summary>
        /// 根据物料获取孔号iHoleIndex（0至11），若该治具不携带该物料则返回-1.
        /// </summary>
        public int this[MaterialItem materialItem]
        {
            get
            {
                return MaterialItems?.IndexOf(materialItem) ?? -1;
            }
        }

        public FixtureItem()
        {
            this.FixtureGuid = Guid.NewGuid();
            this.MaterialItems = new BindingList<MaterialItem>();
            for (int i = 0; i < 12; i++)
            {
                this.MaterialItems.Add(null);
            }
        }

        /// <summary>
        /// 设置当前治具所在设备
        /// </summary>
        /// <param name="fixture"></param>
        public void SetMachineItem(MachineItem machine)
        {
            this.MachineItem = machine;
        }

        public bool InsertMaterialItem(int index, MaterialItem mItem)
        {
            try
            {
                if (index >= 12)
                {
                    throw new Exception("孔号索引不能超过12（正常范围0-11）！");
                }
                if (!this.MaterialItems.Contains(mItem))
                {
                    this.MaterialItems.Insert(index, mItem);
                    mItem.SetFixtureItem(this);
                    return true;
                }
                MaterialItem mpItem = this.MaterialItems[index];
                if (mpItem != null)
                {
                    throw new Exception("插入位置已存在一个物料！");
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveMaterialItem(MaterialItem mItem)
        {
            if (this.MaterialItems.Contains(mItem))
            {
                this.MaterialItems.Remove(mItem);
                mItem.SetFixtureItem(null);
                return true;
            }
            return false;
        }

        public bool RemoveMaterialItemByIndex(int index)
        {
            try
            {
                MaterialItem mItem = this.MaterialItems[index];
                if (mItem != null)
                {
                    mItem.SetFixtureItem(null);
                    this.MaterialItems.RemoveAt(index); //删除旧值
                    this.MaterialItems.Insert(index, null); //插入新值，保持List为12个
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            this.MaterialItems.Clear();
            this.MaterialItems = null;
            this.RFID = null;
            this.FixtureGuid = Guid.Empty;
            this.治具生产批次号 = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect(0);
        }
        #endregion
    }
}