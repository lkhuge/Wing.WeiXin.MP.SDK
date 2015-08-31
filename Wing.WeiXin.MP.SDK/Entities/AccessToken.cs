using System;

namespace Wing.WeiXin.MP.SDK.Entities
{
    /// <summary>
    /// AccessToken对象
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        #region 获取AccessToken描述 public override string ToString()
        /// <summary>
        /// 获取AccessToken描述
        /// </summary>
        /// <returns>AccessToken描述</returns>
        public override string ToString()
        {
            return string.Format("access_token:{0}{2}expires_in:{1}",
                access_token,
                expires_in,
                Environment.NewLine);
        } 
        #endregion
    }
}
