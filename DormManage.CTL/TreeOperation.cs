using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace DormManage.Web.UI.ExtendTree
{
    public class TreeOperation
    {
        public TreeNode FindNode(TreeNode tnParent, string strValue)
        {

            if (tnParent == null) return null;

            if (tnParent.Text == strValue) return tnParent;

            TreeNode tnRet = null;

            foreach (TreeNode tn in tnParent.ChildNodes)
            {

                tnRet = FindNode(tn, strValue);

                if (tnRet != null) break;

            }

            return tnRet;

        }

        /// <summary>
        /// 找到树对应的顶级下级节点
        /// </summary>
        /// <param name="LinksTreeView"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
        public TreeNode Find(TreeView LinksTreeView, string strText)
        {
            if (LinksTreeView.Nodes.Count > 0)
            {

                // Iterate through the root nodes in the Nodes property.
                foreach (TreeNode tn in LinksTreeView.Nodes)
                {

                    // Display the nodes.
                    if (tn.Text == strText)
                    {
                        return tn;
                    }

                }

            }
            return null;

        }



    }
}
