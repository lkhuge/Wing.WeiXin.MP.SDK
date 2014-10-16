using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wing.WeiXin.MP.SDK.Entities.WXXD.PostageTemp
{
    /// <summary>
    /// 微信小店邮费模板
    /// </summary>
    public class WXPostageTemp
    {
        /// <summary>
        /// 邮费模板ID
        /// </summary>
        public int template_id { get; set; }

        /// <summary>
        /// 邮费模板名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 支付方式
        /// (0-买家承担运费, 1-卖家承担运费)
        /// </summary>
        public int Assumer { get; set; }

        /// <summary>
        /// 计费单位
        /// (0-按件计费, 1-按重量计费, 2-按体积计费，目前只支持按件计费，默认为0)
        /// </summary>
        public int Valuation { get; set; }

        /// <summary>
        /// 具体运费计算
        /// </summary>
        public List<PostageTempTopFee> TopFee { get; set; }

        /// <summary>
        /// 具体运费计算
        /// </summary>
        public class PostageTempTopFee
        {
            /// <summary>
            /// 快递类型ID(参见增加商品/快递列表)
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// 默认邮费计算方法
            /// </summary>
            public PostageTempTopFeeNormal Normal { get; set; }

            /// <summary>
            /// 指定地区邮费计算方法
            /// </summary>
            public List<PostageTempTopFeeCustom> Custom { get; set; }

            /// <summary>
            /// 默认邮费计算方法
            /// </summary>
            public class PostageTempTopFeeNormal
            {
                /// <summary>
                /// 起始计费数量
                /// (比如计费单位是按件, 填2代表起始计费为2件)
                /// </summary>
                public int StartStandards { get; set; }

                /// <summary>
                /// 起始计费金额(单位: 分）
                /// </summary>
                public int StartFees { get; set; }

                /// <summary>
                /// 递增计费数量
                /// </summary>
                public int AddStandards { get; set; }

                /// <summary>
                /// 递增计费金额(单位 : 分)
                /// </summary>
                public int AddFees { get; set; }
            }

            /// <summary>
            /// 指定地区邮费计算方法
            /// </summary>
            public class PostageTempTopFeeCustom
            {
                /// <summary>
                /// 起始计费数量
                /// </summary>
                public int StartStandards { get; set; }

                /// <summary>
                /// 起始计费金额(单位: 分）
                /// </summary>
                public int StartFees { get; set; }

                /// <summary>
                /// 递增计费数量
                /// </summary>
                public int AddStandards { get; set; }

                /// <summary>
                /// 递增计费金额(单位 : 分)
                /// </summary>
                public int AddFees { get; set; }

                /// <summary>
                /// 指定国家(详见《地区列表》说明)
                /// </summary>
                public String DestCountry { get; set; }

                /// <summary>
                /// 指定省份(详见《地区列表》说明)
                /// </summary>
                public String DestProvince { get; set; }

                /// <summary>
                /// 指定城市(详见《地区列表》说明)
                /// </summary>
                public String DestCity { get; set; }
            }
        }
    }
}