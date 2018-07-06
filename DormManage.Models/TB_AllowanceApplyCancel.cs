using System;
namespace DormManage.Model.Allowance
{
	/// <summary>
	/// TB_AllowanceApplyCancel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TB_AllowanceApplyCancel
	{
		public TB_AllowanceApplyCancel()
		{}
		#region Model
		private int _id;
		private string _employeeno;
		private string _name;
		private string _cardno;
		private string _sex;
		private string _company;
		private string _bu;
		private int? _grade;
		private int? _allowanceapplyid;
		private string _employeetypename;
		private string _bz;
		private DateTime? _createdate;
		private string _createuser;
		private int? _siteid;
		private DateTime? _hire_date;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmployeeNo
		{
			set{ _employeeno=value;}
			get{return _employeeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company
		{
			set{ _company=value;}
			get{return _company;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BU
		{
			set{ _bu=value;}
			get{return _bu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AlloWanceApplyID
		{
			set{ _allowanceapplyid=value;}
			get{return _allowanceapplyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmployeeTypeName
		{
			set{ _employeetypename=value;}
			get{return _employeetypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BZ
		{
			set{ _bz=value;}
			get{return _bz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SiteID
		{
			set{ _siteid=value;}
			get{return _siteid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Hire_Date
		{
			set{ _hire_date=value;}
			get{return _hire_date;}
		}

        public DateTime? Effective_Date
        {
            get;
            set;
        }
        #endregion Model

    }
}

