// ================================================================ 24.6.0 =====
// Tingen: The Tingen web service
// https://github.com/APrettyCoolProgram/Tingen_development
// Copyright (c) A Pretty Cool Program. All rights reserved.
// Licensed under the Apache 2.0 license.
// ================================================================ 240622 =====

// u240621.0855

/* -----------------------------------------------------------------
 * Important information about Tingen.cs (and Tingen_development.cs)
 * -----------------------------------------------------------------
 *
 * Tingen.cs and Tingen_development.cs are the entry points for the Tingen web service. Tingen.cs is the stable release intended for
 * production environments, while Tingen_development.cs is the development version.
 *
 * You are currently viewing Tingen.cs.
 *
 * These classes are pretty bare-bones because the heavy lifting is done in Outpost31, which is shared between the production and
 * development version of Tingen.
 *
 * Tingen.cs/Tingen_development.cs should not be modified, so don't worry if the "// uYYMMDD.HHMM" comment up above is old.
 *
 * Any changes to the Tingen web service should be made in Outpost31, generally in TingenApp.Start() and TingenApp.Stop().
 */

using System.Reflection;
using System.Web.Services;
using Outpost31.Core;
using Outpost31.Core.Logger;
using Outpost31.Core.Session;
using ScriptLinkStandard.Objects;

namespace Tingen
{
    /// <summary>
    /// Summary description for Tingen
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Tingen : System.Web.Services.WebService
    {
        /// <summary>Assembly name for log files.</summary>
        /// <remarks>
        ///   <para>
        ///    - Define the assembly name here so it can be used to write log files throughout the class.
        ///   </para>
        /// </remarks>
        public static string AssemblyName { get; set; } = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>The current version of Tingen.</summary>
        /// <remarks>
        ///   <para>
        ///    - This is used in a few places in this class, so let's define it here.
        ///   </para>
        /// </remarks>
        public static string TingenVersion { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>Returns the current version of Tingen.</summary>
        /// <remarks>
        ///  <para>
        ///   - Required by Avatar.<br/>
        ///   - <i>Should not be modified</i>.
        ///  </para>
        /// </remarks>
        /// <returns>The current version of Tingen.</returns>
        [WebMethod]
        public string GetVersion() => $"VERSION {TingenVersion}";

        /// <summary>Starts the Tingen web service</summary>
        /// <param name="sentOptionObject">The OptionObject sent from Avatar.</param>
        /// <param name="sentScriptParameter">The Script Parameter sent from Avatar.</param>
        /// <remarks>
        ///  <para>
        ///   - Required by Avatar.<br/>
        ///   - <i>Should not be modified</i><br/>
        ///   - The majority of work is done in the <see href="github.com/spectrum-health-systems/Tingen-Documentation/blob/main/Glossary.md#Outpost31">Outpost31</see> project.
        ///  </para>
        /// </remarks>
        /// <returns>The finalized OptionObject to myAvatar.</returns>
        [WebMethod]
        public OptionObject2015 RunScript(OptionObject2015 sentOptionObject, string sentScriptParameter)
        {
            /* Trace logs can't go here - the infrastrucure isn't setup yet. */

            TingenSession tnSession = TingenSession.Build(sentOptionObject, sentScriptParameter, TingenVersion);

            LogEvent.Trace(1, AssemblyName, tnSession.TraceInfo);

            TingenApp.Start(tnSession);

            TingenApp.Stop(tnSession);

            return tnSession.AvData.ReturnOptionObject;
        }
    }
}
