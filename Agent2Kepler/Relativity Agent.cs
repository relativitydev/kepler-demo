﻿using kCura.Agent;
using kCura.Relativity.Client;
using Kepler_Demo.Interfaces.Demo.v1;
using Relativity.API;
using System;
using System.Net;

namespace Agent2Kepler
{
	[kCura.Agent.CustomAttributes.Name("Agent2Kepler")]
	[System.Runtime.InteropServices.Guid("cb4953cf-7a1c-4fad-bab0-e449d40b16a4")]
	public class Relativity_Agent : AgentBase
	{
		/// <summary>
		/// Agent logic goes here
		/// </summary>
		public override void Execute()
		{
			IAPILog logger = Helper.GetLoggerFactory().GetLogger();

			try
			{
				// Update Security Protocol
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

				//Get the current Agent artifactID
				int agentArtifactId = AgentID;

				//Get a dbContext for the EDDS database
				IDBContext eddsDbContext = Helper.GetDBContext(-1);

				//Get a dbContext for the workspace database
				//int workspaceArtifactId = 01010101; // Update it with the real 
				//IDBContext workspaceDbContext = Helper.GetDBContext(workspaceArtifactId);

				//Get GUID for an artifact
				//int testArtifactId = 10101010;
				//Guid guidForTestArtifactId = Helper.GetGuid(workspaceArtifactId, testArtifactId);

				//Display a message in Agents Tab and Windows Event Viewer
				RaiseMessage("The current time is: " + DateTime.Now.ToLongTimeString(), 1);

				//The Object Manager is the newest and preferred way to interact with Relativity instead of the Relativity Services API(RSAPI). 
				//The RSAPI will be scheduled for depreciation after the Object Manager reaches feature party with it.
				using (IWorkspaceName workspaceName = this.Helper.GetServicesManager().CreateProxy<IWorkspaceName>(ExecutionIdentity.CurrentUser))
				{
					String name = workspaceName.GetWorkspaceNameAsync(1017759).Result.Name;
					RaiseMessage("The worksapce name is" + name, 1);
				}

				//Setting up an RSAPI Client
				using (IRSAPIClient rsapiClient = Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.CurrentUser))
				{
					//Set the proxy to use the current workspace
					//rsapiClient.APIOptions.WorkspaceID = workspaceArtifactId;

					//Add code for working with RSAPIClient
				}

				logger.LogVerbose("Log information throughout execution.");
			}
			catch (Exception ex)
			{
				//Your Agent caught an exception
				logger.LogError(ex, "There was an exception.");
				RaiseError(ex.Message, ex.Message);
			}
		}

		/// <summary>
		/// Returns the name of agent
		/// </summary>
		public override string Name
		{
			get
			{
				return "Agent Name";
			}
		}
	}
}