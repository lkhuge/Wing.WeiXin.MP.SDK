namespace Wing.WeiXin.MP.SDK.Entities.Template
{
    /// <summary>
    /// 信息模板返回信息
    /// </summary>
    public class MessageTemplateReturnMessage : ErrorMsg
    {
        /// <summary>
        /// 消息发送任务的ID
        /// </summary>
        public long msgid { get; set; }
    }
}
