using Meting4Net.Core;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using Native.Tool.Http;
using Native.Tool.IniConfig;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Text;

namespace com.moeci.music.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        /// <summary>
        /// 收到群消息
        /// </summary>
        /// <param name="sender">事件来源</param>
        /// <param name="e">事件参数</param>
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            // 获取 At 某人对象
            CQCode cqat = e.FromQQ.CQCode_At();

            string message = e.Message.Text;
            bool isSong = false;
            StringBuilder sbSendMsg = new StringBuilder();

            #region 音乐搜索
            if (message.Contains("音乐搜索"))
            {
                isSong = true;

                Meting api = null;
                string songKw = string.Empty;
                if (message.Contains("网易云音乐搜索"))
                {
                    api = new Meting(ServerProvider.Netease);
                    songKw = message.Replace("网易云音乐搜索", "").Trim();
                }
                else if (message.Contains("QQ音乐搜索"))
                {
                    api = new Meting(ServerProvider.Tencent);
                    songKw = message.Replace("QQ音乐搜索", "").Trim();
                }
                else if (message.Contains("酷狗音乐搜索"))
                {
                    api = new Meting(ServerProvider.Kugou);
                    songKw = message.Replace("酷狗音乐搜索", "").Trim();
                }
                else if (message.Contains("虾米音乐搜索"))
                {
                    api = new Meting(ServerProvider.Xiami);
                    songKw = message.Replace("虾米音乐搜索", "").Trim();
                }
                else if (message.Contains("百度音乐搜索"))
                {
                    api = new Meting(ServerProvider.Baidu);
                    songKw = message.Replace("百度音乐搜索", "").Trim();
                }


                try
                {
                    var result = api.SearchObj(songKw);
                    if (result.Count() > 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var item = result[i];
                            sbSendMsg.Append($"{item.name} 歌曲ID: {item.id}\n");
                        }
                    }
                    else
                    {
                        foreach (var item in result)
                        {
                            sbSendMsg.Append($"{item.name} 歌曲ID: {item.id}\n");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                }
            }
            #endregion

            #region 音乐下载
            if (message.Contains("音乐下载"))
            {
                isSong = true;
                Meting api = null;
                string songId = string.Empty;

                if (message.Contains("网易云音乐下载"))
                {
                    api = new Meting(ServerProvider.Netease);
                    songId = message.Replace("网易云音乐下载", "").Trim();
                }
                else if (message.Contains("QQ音乐下载"))
                {
                    api = new Meting(ServerProvider.Tencent);
                    songId = message.Replace("QQ音乐下载", "").Trim();
                }
                else if (message.Contains("酷狗音乐下载"))
                {
                    api = new Meting(ServerProvider.Kugou);
                    songId = message.Replace("酷狗音乐下载", "").Trim();
                }
                else if (message.Contains("虾米音乐下载"))
                {
                    api = new Meting(ServerProvider.Xiami);
                    songId = message.Replace("虾米音乐下载", "").Trim();
                }
                else if (message.Contains("百度音乐下载"))
                {
                    api = new Meting(ServerProvider.Baidu);
                    songId = message.Replace("百度音乐下载", "").Trim();
                }

                try
                {
                    var result = api.SongObj(songId);
                    string url = api.UrlObj(result.url_id).url;

                    #region 缩短链接
                    // 缩短链接
                    //try
                    //{
                    //    string urlEncode = HttpTool.UrlEncode(url);
                    //    //byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes($"Url={urlEncode}&TermOfValidity=long-term");
                    //    string sendJson = JsonConvert.SerializeObject(new { Url = url });
                    //    byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(sendJson);
                    //    WebHeaderCollection webHeader = new WebHeaderCollection();
                    //    webHeader.Add("Token", "1d960a9d42c811886a724d50782903c7");
                    //    byte[] bytes = HttpWebClient.Post(url: $"https://dwz.cn/admin/v2/create", contentType: "application/json; charset=UTF-8", data: sendBytes, headers: ref webHeader);

                    //    // {"Code":-1,"LongUrl":"http://m7.music.126.net/20200529000223/8b5c50f306065de479d8c9806c4278a9/ymusic/000c/5259/070c/7f82130392e3835ddfbf0f2a0ed84de5.mp3","ErrMsg":"enterprise qualification is needed"}
                    //    string jsonStr = System.Text.Encoding.UTF8.GetString(bytes);
                    //    dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(jsonStr);
                    //    if (jsonObj != null && jsonObj.Code == 0 || jsonObj.ErrMsg == "")
                    //    {
                    //        url = jsonObj.ShortUrl;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{ }
                    #endregion


                    sbSendMsg.Append($"{result.name} 下载\n {url}");

                }
                catch (System.Exception ex)
                {
                }
            }
            #endregion

            #region 音乐歌词
            if (message.Contains("音乐歌词"))
            {
                isSong = true;
                Meting api = null;
                string songId = string.Empty;

                if (message.Contains("网易云音乐歌词"))
                {
                    api = new Meting(ServerProvider.Netease);
                    songId = message.Replace("网易云音乐歌词", "").Trim();
                }
                else if (message.Contains("QQ音乐歌词"))
                {
                    api = new Meting(ServerProvider.Tencent);
                    songId = message.Replace("QQ音乐歌词", "").Trim();
                }
                else if (message.Contains("酷狗音乐歌词"))
                {
                    api = new Meting(ServerProvider.Kugou);
                    songId = message.Replace("酷狗音乐歌词", "").Trim();
                }
                else if (message.Contains("虾米音乐歌词"))
                {
                    api = new Meting(ServerProvider.Xiami);
                    songId = message.Replace("虾米音乐歌词", "").Trim();
                }
                else if (message.Contains("百度音乐歌词"))
                {
                    api = new Meting(ServerProvider.Baidu);
                    songId = message.Replace("百度音乐歌词", "").Trim();
                }

                try
                {
                    var result = api.SongObj(songId);
                    var song = api.LyricObj(songId);
                    string lyric = song.lyric;
                    if (string.IsNullOrEmpty(lyric))
                    {
                        lyric = song.tlyric;
                    }

                    sbSendMsg.Append("已私聊发送歌词");

                    e.FromQQ.SendPrivateMessage($"{result.name} 歌词\n {lyric}");
                }
                catch (System.Exception ex)
                {
                }
            }
            #endregion

            if (isSong)
            {
                e.FromGroup.SendGroupMessage(cqat, "\n" + sbSendMsg.ToString());
            }

            // 设置该属性, 表示阻塞本条消息, 该属性会在方法结束后传递给酷Q
            e.Handler = true;
        }
    }
}
