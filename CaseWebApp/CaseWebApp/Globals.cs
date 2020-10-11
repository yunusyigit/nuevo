using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseWebApp
{
    public class Globals
    {

        public static void SetGlobals(IConfiguration configuration)
        {
            CONTENT_FOLDER_ROOT_PATH = "..";
        }
        public static string CONTENT_FOLDER_ROOT_PATH { get; private set; }

        public static string Session_UserName = "SessionUserName";
        public static string  Session_UserId = "SessionUserId";
    }
}
