using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Wing.WeiXin.MP.SDK.Entities.ReturnMessages
{
    /// <summary>
    /// 回复消息抽象类
    /// </summary>
    public abstract class BaseReturnMessage : BaseEntity, IReturn
    {
    }
}
