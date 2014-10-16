using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wing.WeiXin.MP.SDK.Entities;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.GoodsShelf
{
    /// <summary>
    /// 微信小店货架
    /// </summary>
    public class WXGoodsShelf : ErrorMsg
    {
        /// <summary>
        /// 货架ID
        /// </summary>
        public int shelf_id { get; set; }

        /// <summary>
        /// 货架信息(数据说明详见《货架控件说明》)
        /// </summary>
        public WXGoodsShelfData shelf_data { get; set; }

        /// <summary>
        /// 货架招牌图片Url
        /// (图片需调用图片上传接口获得图片Url填写至此，否则添加货架失败，
        /// 建议尺寸为640*120，仅控件1-4有banner，控件5没有banner)
        /// </summary>
        public String shelf_banner { get; set; }

        /// <summary>
        /// 货架名称
        /// </summary>
        public String shelf_name { get; set; }

        /// <summary>
        /// 货架信息
        /// </summary>
        public class WXGoodsShelfData
        {
            /// <summary>
            /// 货架控件
            /// </summary>
            public List<WXGoodsShelfDataModule> module_infos { get; set; }

            #region 货架控件
            /// <summary>
            /// 货架控件
            /// </summary>
            public abstract class WXGoodsShelfDataModule
            {
                /// <summary>
                /// 货架控件分组编号
                /// </summary>
                public int eid { get; set; }
            } 
            #endregion

            #region 货架控件分组1
            /// <summary>
            /// 货架控件分组1
            /// </summary>
            public class WXGoodsShelfDataModuleGroup1 : WXGoodsShelfDataModule
            {
                /// <summary>
                /// 初始化空的货架控件分组1
                /// </summary>
                public WXGoodsShelfDataModuleGroup1()
                {
                    eid = 1;
                }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public WXGoodsShelfDataModuleGroup group_info { get; set; }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public class WXGoodsShelfDataModuleGroup
                {
                    /// <summary>
                    /// 分类
                    /// </summary>
                    public WXGoodsShelfDataModuleGroupFilter filter { get; set; }

                    /// <summary>
                    /// 分组ID
                    /// </summary>
                    public int group_id { get; set; }

                    /// <summary>
                    /// 分类
                    /// </summary>
                    public class WXGoodsShelfDataModuleGroupFilter
                    {
                        /// <summary>
                        /// 该控件展示商品个数
                        /// </summary>
                        public int count { get; set; }
                    }
                }
            } 
            #endregion

            #region 货架控件分组2
            /// <summary>
            /// 货架控件分组2
            /// </summary>
            public class WXGoodsShelfDataModuleGroup2 : WXGoodsShelfDataModule
            {
                /// <summary>
                /// 初始化空的货架控件分组2
                /// </summary>
                public WXGoodsShelfDataModuleGroup2()
                {
                    eid = 2;
                }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public WXGoodsShelfDataModuleGroup group_infos { get; set; }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public class WXGoodsShelfDataModuleGroup
                {
                    /// <summary>
                    /// 分类
                    /// </summary>
                    public List<WXGoodsShelfDataModuleGroupGroup> groups { get; set; }

                    /// <summary>
                    /// 分类
                    /// </summary>
                    public class WXGoodsShelfDataModuleGroupGroup
                    {
                        /// <summary>
                        /// 分组ID
                        /// </summary>
                        public int group_id { get; set; }
                    }
                }
            } 
            #endregion

            #region 货架控件分组3
            /// <summary>
            /// 货架控件分组3
            /// </summary>
            public class WXGoodsShelfDataModuleGroup3 : WXGoodsShelfDataModule
            {
                /// <summary>
                /// 初始化空的货架控件分组3
                /// </summary>
                public WXGoodsShelfDataModuleGroup3()
                {
                    eid = 3;
                }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public WXGoodsShelfDataModuleGroup group_info { get; set; }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public class WXGoodsShelfDataModuleGroup
                {
                    /// <summary>
                    /// 分组ID
                    /// </summary>
                    public int group_id { get; set; }

                    /// <summary>
                    /// 分组照片
                    /// (图片需调用图片上传接口获得图片Url填写至此，否则添加货架失败，建议分辨率600*208)
                    /// </summary>
                    public String img { get; set; }
                }
            } 
            #endregion

            #region 货架控件分组4
            /// <summary>
            /// 货架控件分组4
            /// </summary>
            public class WXGoodsShelfDataModuleGroup4 : WXGoodsShelfDataModule
            {
                /// <summary>
                /// 初始化空的货架控件分组4
                /// </summary>
                public WXGoodsShelfDataModuleGroup4()
                {
                    eid = 4;
                }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public WXGoodsShelfDataModuleGroup group_infos { get; set; }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public class WXGoodsShelfDataModuleGroup
                {
                    /// <summary>
                    /// 分类
                    /// </summary>
                    public WXGoodsShelfDataModuleGroupGroup groups { get; set; }

                    /// <summary>
                    /// 分类
                    /// </summary>
                    public class WXGoodsShelfDataModuleGroupGroup
                    {
                        /// <summary>
                        /// 分组ID
                        /// </summary>
                        public int group_id { get; set; }

                        /// <summary>
                        /// 分组照片
                        /// (图片需调用图片上传接口获得图片Url填写至此，否则添加货架失败，3个分组建议分辨率分别为: 350*350, 244*172, 244*172)
                        /// </summary>
                        public String img { get; set; }
                    }
                }
            } 
            #endregion

            #region 货架控件分组5
            /// <summary>
            /// 货架控件分组5
            /// </summary>
            public class WXGoodsShelfDataModuleGroup5 : WXGoodsShelfDataModule
            {
                /// <summary>
                /// 初始化空的货架控件分组5
                /// </summary>
                public WXGoodsShelfDataModuleGroup5()
                {
                    eid = 5;
                }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public WXGoodsShelfDataModuleGroup group_infos { get; set; }

                /// <summary>
                /// 货架控件分组
                /// </summary>
                public class WXGoodsShelfDataModuleGroup
                {
                    /// <summary>
                    /// 分类
                    /// </summary>
                    public WXGoodsShelfDataModuleGroupGroup groups { get; set; }

                    /// <summary>
                    /// 分组照片
                    /// (图片需调用图片上传接口获得图片Url填写至此，否则添加货架失败，3个分组建议分辨率分别为: 350*350, 244*172, 244*172)
                    /// </summary>
                    public String img_background { get; set; }

                    /// <summary>
                    /// 分类
                    /// </summary>
                    public class WXGoodsShelfDataModuleGroupGroup
                    {
                        /// <summary>
                        /// 分组ID
                        /// </summary>
                        public int group_id { get; set; }
                    }
                }
            }
            #endregion
        }
    }
}