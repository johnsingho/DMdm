using System;
using System.Collections.Generic;
using System.Text;
using DormManage.Framework.Enum;

namespace DormManage.Framework
{
    public class Pager
    {
        private int mCurrentPageIndex = 0;//mCurrentPageIndex=-9 表示取出数据集的最后一页 目前只有mysql已经实现
        public int PageSize = 10;
        public string srcOrder = string.Empty;
        public int TotalRecord = 0;
        public bool IsNull = false;
        private string sqlpagerNew = @"SELECT * FROM ({sqlmain}) B {order0} LIMIT {skip},{pagesize}";

        public int CurrentPageIndex
        {
            get
            {
                return mCurrentPageIndex;
            }
            set
            {
                mCurrentPageIndex = value;
            }
        }

        /// <summary>
        /// 指定排序的分页语句
        /// </summary>
        /// <param name="srcSql"></param>
        /// <param name="srcOrder"></param>
        /// <returns></returns>
        public string GetPagerSql4Data(string srcSql)
        {
            return new SqlStringHelper().GetPagerSql(srcSql, srcOrder, this.CurrentPageIndex * PageSize, this.PageSize);
        }

        public string GetSQLServerData(string srcSql)
        {
            string sortPageSql = @"SELECT * FROM (
                                    SELECT *,ROW_NUMBER() OVER(ORDER BY {0}) AS rowNumber
                                    FROM (
                                    {1}
                                    ) dd ) tt WHERE {2}";
            string whereRowNumber;
            if (CurrentPageIndex == 1)
            {
                whereRowNumber = "rowNumber<=" + PageSize.ToString();
            }
            else if (CurrentPageIndex == TotalPage)
            {
                whereRowNumber = "rowNumber>" + ((CurrentPageIndex - 1) * PageSize).ToString();
            }
            else
            {
                whereRowNumber = "rowNumber>" + ((CurrentPageIndex - 1) * PageSize).ToString()
                            + " AND rowNumber<=" + (CurrentPageIndex * PageSize).ToString();
            }
            return string.Format(sortPageSql, srcOrder, srcSql, whereRowNumber);
        }


        public string GetPagerSql4Data(string srcSql, DataBaseTypeEnum dbType)
        {
            if (dbType == DataBaseTypeEnum.mysql)
            {
                srcSql = srcSql.Trim();
                if (srcSql.StartsWith("select ", StringComparison.CurrentCultureIgnoreCase) || srcSql.StartsWith("select\n", StringComparison.CurrentCultureIgnoreCase))
                {
                    srcSql = sqlpagerNew.Replace("{sqlmain}", srcSql);
                    srcSql = srcSql.Replace("{pagesize}", Convert.ToString(this.PageSize));
                    srcSql = srcSql.Replace("{skip}", Convert.ToString((this.CurrentPageIndex - 1) * this.PageSize));
                    if (this.srcOrder.Length > 0)
                    {
                        srcSql = srcSql.Replace("{order0}", " order by " + this.srcOrder);
                    }
                    else
                    {
                        //srcSql = srcSql.Replace("{order0}", " order by id");
                        srcSql = srcSql.Replace("{order0}", " ");
                    }
                    //srcSql = "select top " + Convert.ToString(this.PageSize * this.CurrentPageIndex) + srcSql.Substring(7);
                    //srcSql = sqlpagermode.Replace("{sqlmain}",srcSql);

                    //if (this.PageSize * this.CurrentPageIndex > TotalRecord)
                    //{
                    //    srcSql = srcSql.Replace("{pagesize}", Convert.ToString(TotalRecord - this.PageSize *( this.CurrentPageIndex-1)));
                    //}
                    //else
                    //{
                    //    srcSql = srcSql.Replace("{pagesize}", this.PageSize.ToString());
                    //}

                    //if (this.srcOrder.Length > 0)
                    //{
                    //    srcSql = srcSql.Replace("{order0}", " order by " + this.srcOrder);
                    //    srcSql = srcSql.Replace("{order1}", " order by " + this.GetOrder2());
                    //}
                    //else
                    //    srcSql = srcSql.Replace("{order1}", "");

                    return srcSql;
                }
            }
            else if (dbType == DataBaseTypeEnum.sqlserver)
            {
                srcSql = srcSql.Trim();
                if (srcSql.StartsWith("select ", StringComparison.CurrentCultureIgnoreCase) || srcSql.StartsWith("select\n", StringComparison.CurrentCultureIgnoreCase))
                {
                    srcSql = this.GetSQLServerData(srcSql);
                    return srcSql;
                }
            }
            else if (dbType == DataBaseTypeEnum.oracle)
            {
                //srcSql = srcSql.Trim();
                //if (this.srcOrder.Length > 0)
                //{
                //    srcSql += " order by " + this.srcOrder;
                //}

                //if(this.mCurrentPageIndex==-9)//the last page
                //    srcSql += " limit " + this.PageSize * (this.TotalPage-1) + "," + this.PageSize.ToString();
                //else
                //    srcSql += " limit " + this.PageSize * (this.mCurrentPageIndex - 1) + "," + this.PageSize.ToString();

                int beginNum = this.PageSize * (this.CurrentPageIndex - 1) + 1;
                //if (beginNum < 0)
                //    beginNum = 0;
                int endNum = this.PageSize * this.CurrentPageIndex;
                string strSql = "SELECT * FROM (SELECT ROWNUM RN,A.* FROM (" + srcSql + ") A ) WHERE RN BETWEEN {0} AND {1}";
                strSql = string.Format(strSql, beginNum, endNum);
                return strSql;
            }
            throw new Exception("非法分页" + srcSql);
        }

        private string GetOrder2()
        {
            string temp = srcOrder;
            temp = temp.Replace(" desc", " ___a_sc");
            temp = temp.Replace(" asc", " ___d_esc");
            temp = temp.Replace(" ___a_sc", " asc");
            temp = temp.Replace(" ___d_esc", " desc");
            return temp;
        }

        public string GetPagerSql4Count(string srcSql)
        {
            return " select count(1) from (" + srcSql + ") tb";
        }

        /// <summary>
        /// 无指定排序的分页语句
        /// </summary>
        /// <param name="srcSql"></param>
        /// <param name="srcOrder"></param>
        /// <returns></returns>
        public string GetPagerSql(string srcSql)
        {
            return new SqlStringHelper().GetPagerSql(srcSql, srcOrder, this.CurrentPageIndex * PageSize, this.PageSize);
        }

        public int TotalPage
        {
            get
            {
                if (TotalRecord % PageSize == 0)
                {
                    return this.TotalRecord / PageSize;
                }
                else
                {
                    return this.TotalRecord / PageSize + 1;
                }
            }
        }





        /// <summary>
        /// 取得下一页的Pager
        /// </summary>
        public Pager NextPage
        {
            get
            {
                this.CurrentPageIndex++;
                return this;
            }
        }

        /// <summary>
        /// 取得前一页的Pager
        /// </summary>
        public Pager PreviousPage
        {
            get
            {
                this.CurrentPageIndex--;
                return this;
            }
        }

        public Pager LastPage
        {
            get
            {
                this.CurrentPageIndex = TotalPage;
                return this;
            }
        }

        public Pager FirstPage
        {
            get
            {
                this.CurrentPageIndex = 0;
                return this;
            }
        }
    }


    class SqlStringHelper
    {
        private string _strOrder = null;
        private int _iMaxPop = 0;
        private int _iPageSize = 0;

        //private string _strOrder2 = "";

        private string strHeader
        {
            get
            {
                return "  Select *  from"
                        + "(  "
                        + "SELECT * FROM ("
                        + "SELECT  rownum r, A.*   FROM "
                        + "(  ";
            }
        }

        private string strFooter
        {
            get
            {
                if (_strOrder == null)
                    _strOrder = "";

                //_strOrder2 = "";

                //if (_strOrder.Length > 0)
                //{
                //    _strOrder = _strOrder.ToLower();
                //    _strOrder2=_strOrder.Replace(" desc", " a-_--sc");
                //    _strOrde2r=_strOrder2.Replace(" asc", " d-_--esc");
                //    _strOrder2=_strOrder2.Replace("-_--", "");
                //}

                return "  ) A WHERE rownum <= " + _iMaxPop +
                    ") B WHERE r > " + (_iMaxPop - _iPageSize) + " " + this._strOrder +
                ") ";
            }
        }

        public string GetPagerSql(string srcSql, string strOrder, int iMaxPop, int iPageSize)
        {
            if (strOrder != null && strOrder.Length > 0)
                strOrder = " order by " + strOrder;
            _strOrder = strOrder;
            srcSql = srcSql + " " + strOrder + " ";
            _iMaxPop = iMaxPop;
            _iPageSize = iPageSize;

            return strHeader + srcSql + strFooter;
        }
    }

}
