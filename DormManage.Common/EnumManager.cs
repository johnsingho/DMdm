using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DormManage.Common
{
    /// <summary>
    /// 模块操作ID,增删改等
    /// </summary>
    public enum Operations
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,

        /// <summary>
        /// 浏览或查询
        /// </summary>
        Search = 2,

        /// <summary>
        /// 删除
        /// </summary>
        Remove = 3,

        /// <summary>
        /// 全部删除
        /// </summary>
        RemoveAll = 4,

        /// <summary>
        /// 导入
        /// </summary>
        Inport = 5,

        /// <summary>
        /// 导出
        /// </summary>
        Export = 6,

    }

    /// <summary>
    /// 描  述：模块与数据库对应
    /// 作  者：
    /// 时  间：
    /// 修  改：
    /// 原  因：
    /// </summary>
    public enum KFModules
    {
        #region 文件
        /// <summary>
        /// 文件
        /// </summary>
        Files = 143,

        /// <summary>
        /// 打开
        /// </summary>
        FileOpen = 144,

        /// <summary>
        /// 新窗口打开
        /// </summary>
        FileOpenNewWindow = 145,

        /// <summary>
        /// 新建
        /// </summary>
        FileNew = 146,

        /// <summary>
        /// 授权
        /// </summary>
        FileAuthorize = 147,

        /// <summary>
        /// 删除
        /// </summary>
        FileDelete = 148,

        /// <summary>
        /// 重命名
        /// </summary>
        FileRename = 149,

        /// <summary>
        /// 属性
        /// </summary>
        FileProperty = 190,

        /// <summary>
        /// 站点管理
        /// </summary>
        FileSiteManage = 191,

        /// <summary>
        /// 退出
        /// </summary>
        FileExit = 192,

        /// <summary>
        /// 上传
        /// </summary>
        FileUpLoad = 183,

        /// <summary>
        /// 申请借阅
        /// </summary>
        FileRequreBorrow = 184,

        /// <summary>
        /// 借阅
        /// </summary>
        FileBorrow = 185,

        /// <summary>
        /// 申请下载
        /// </summary>
        FileRequreDownload = 186,

        /// <summary>
        /// 下载
        /// </summary>
        FileDownLoad = 187,

        /// <summary>
        /// 设置缺省
        /// </summary>
        FileSetAsDefault = 188,

        #endregion

        #region  编辑

        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 153,

        /// <summary>
        /// 撤销
        /// </summary>
        Cancel = 154,

        /// <summary>
        /// 剪切
        /// </summary>
        Cut = 155,

        /// <summary>
        /// 复制
        /// </summary>
        Copy = 156,

        /// <summary>
        /// 黏贴
        /// </summary>
        Pause = 157,

        /// <summary>
        /// 全选
        /// </summary>
        SelectAll = 158,

        #endregion

        #region  视图

        /// <summary>
        /// 视图
        /// </summary>
        View = 159,

        /// <summary>
        /// 列表
        /// </summary>
        ViewList = 160,

        /// <summary>
        /// 平铺
        /// </summary>
        ViewTile = 161,

        /// <summary>
        /// 详细信息
        /// </summary>
        ViewDetail = 162,

        /// <summary>
        /// 排列方式
        /// </summary>
        ViewArrangement = 163,

        /// <summary>
        /// 工具栏
        /// </summary>
        ViewTools = 164,

        /// <summary>
        /// 信息栏
        /// </summary>
        ViewInfomation = 165,

        /// <summary>
        /// 目录树
        /// </summary>
        ViewDirectoryTree = 166,

        /// <summary>
        /// 刷新
        /// </summary>
        ViewRefresh = 167,

        #endregion

        #region 选项

        /// <summary>
        /// 选项
        /// </summary>
        Options = 168,

        /// <summary>
        /// 全局选项
        /// </summary>
        OptionGlobal = 169,

        /// <summary>
        /// 账号配置
        /// </summary>
        OptionAccountConfiguration = 170,

        /// <summary>
        /// 联系人
        /// </summary>
        OptionContact = 171,

        /// <summary>
        /// 文件夹选项
        /// </summary>
        OptionFolder=172,


        #endregion

        #region 工具栏

        /// <summary>
        /// 工具
        /// </summary>
        Tools = 173,

        /// <summary>
        /// 阅读器
        /// </summary>
        ToolReader = 174,

        /// <summary>
        /// 回收站
        /// </summary>
        Recycle = 175,

        /// <summary>
        /// 工作台
        /// </summary>
        ToolWorkPlatForm = 189,

        /// <summary>
        /// 我的工单
        /// </summary>
        ToolMyForm = 176,

        /// <summary>
        /// 新建工单
        /// </summary>
        ToolNewForm= 177,

        /// <summary>
        /// 待办工单
        /// </summary>
        ToolTaskForm = 178,

        /// <summary>
        /// 已办工单
        /// </summary>
        ToolCompletForm = 179,

        #endregion

        #region 帮助

        /// <summary>
        /// 帮助
        /// </summary>
        Helps = 180,

        /// <summary>
        /// 帮助主题
        /// </summary>
        HelpSubject = 181,

        /// <summary>
        /// 关于
        /// </summary>
        HelpAbout = 182,

        #endregion
    }
}
