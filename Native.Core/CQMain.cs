using Native.Sdk.Cqp.Interface;
using com.moeci.music.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Native.Core
{
	/// <summary>
	/// 酷Q应用主入口类
	/// </summary>
	public class CQMain
	{
		/// <summary>
		/// 在应用被加载时将调用此方法进行事件注册, 请在此方法里向 <see cref="IUnityContainer"/> 容器中注册需要使用的事件
		/// </summary>
		/// <param name="container">用于注册的 IOC 容器 </param>
		public static void Register (IUnityContainer unityContainer)
		{
			// 在 Json 中, 群消息的 name 字段是: 群消息处理, 因此这里注册的第一个参数也是这样填写
			unityContainer.RegisterType<IGroupMessage, Event_GroupMessage>("群消息处理");
		}
	}
}
