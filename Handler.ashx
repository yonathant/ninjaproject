<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(context.Server.MapPath("~/trees/tree.xml"));


        string gameCode = context.Request["gameCode"]; // חשוב לשים לב שזה אותו שם משתנה כמו באנימייט

        XmlNode gameNode = myDoc.SelectSingleNode("//game[@gid='" + gameCode + "']");


         if (gameNode != null)
         {
            if (gameNode.Attributes["publish"].Value == "False")
            {
                context.Response.Write("notPublished");
            }
            else
            {
                string jsonText = JsonConvert.SerializeXmlNode(gameNode);

                context.Response.Write(jsonText);
            }

        }
        else
        {
            context.Response.Write("noGame");
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
















