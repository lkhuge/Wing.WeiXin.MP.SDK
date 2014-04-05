namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages.ReturnObject
{
    /// <summary>
    /// 视频对象
    /// </summary>
    public class Video
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频消息的描述 
        /// </summary>
        public string Description { get; set; }
    }
}
