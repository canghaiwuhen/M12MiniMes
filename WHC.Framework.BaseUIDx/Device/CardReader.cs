﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WHC.Framework.BaseUI
{
    /// <summary>
    /// 读卡器封装类
    /// </summary>
    public class CardReader
    {
        private Control _hostCtrl;
        private string _cardCode;
        private Timer _timer;
        private const int CARD_CODE_LEN = 10;
        private const string CARD_CODE_START = "00";

        /// <summary>
        /// 读卡器读到一张卡的事件
        /// </summary>
        public event CardReadEventHandler CardRead;

        /// <summary>
        /// 默认读卡器（挂在主窗体上，会被主窗体初始化，在模块里用肯定是安全的）
        /// </summary>
        public static CardReader Default { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="hostCtrl">接受键盘事件的宿主控件</param>
        public CardReader(Control hostCtrl)
        {
            _hostCtrl = hostCtrl;
            if (_hostCtrl is Form)
            {
                (_hostCtrl as Form).KeyPreview = true;
            }
            _hostCtrl.KeyUp += new KeyEventHandler(hostCtrl_KeyUp);
            _cardCode = "";
            _timer = new Timer();
            _timer.Interval = 20;
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Start();
        }

        /// <summary>
        /// 判断是否卡号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsCardCode(string code)
        {
            return code.Length == CARD_CODE_LEN && code.StartsWith(CARD_CODE_START);
        }

        /// <summary>
        /// 定时器到期的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            //达到一定的位数才开始判断
            if (_cardCode.Length >= CARD_CODE_LEN)
            {
                _cardCode = _cardCode.Trim((char)13);
                if (IsCardCode(_cardCode))
                {
                    _timer.Stop();
                    OnCardRead(_cardCode);
                }
            }
            _cardCode = "";
            _timer.Start();
        }

        /// <summary>
        /// 监听按键弹起的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hostCtrl_KeyUp(object sender, KeyEventArgs e)
        {
            _timer.Stop();
            _cardCode = _cardCode + (char)e.KeyValue;
            _timer.Start();
        }

        private void OnCardRead(string scanCode)
        {
            if (CardRead != null)
            {
                CardRead(scanCode);
            }
        }

    }

    /// <summary>
    /// 读卡器读到一张卡的事件处理委托
    /// </summary>
    /// <param name="cardCode"></param>
    public delegate void CardReadEventHandler(string cardCode);
}
