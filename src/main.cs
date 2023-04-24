using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.API.Util;

namespace realmsofandora
{
	public class bedspawnner : ModSystem
	{
		private ICoreServerAPI serverApi;
		private int[] bedIDs;

		public override void StartServerSide(ICoreServerAPI api)
		{
			this.serverApi = api;
			this.bedIDs = new int[] {
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-head-north")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-head-north")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-head-north")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-feet-north")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-feet-north")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-feet-north")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-head-south")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-head-south")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-head-south")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-feet-south")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-feet-south")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-feet-south")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-head-east")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-head-east")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-head-east")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-feet-east")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-feet-east")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-feet-east")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-head-west")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-head-west")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-head-west")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-hay-feet-west")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-wood-feet-west")).BlockId,
				this.serverApi.World.GetBlock(new AssetLocation("bed-woodaged-feet-west")).BlockId
			};
			
			api.Event.DidUseBlock += OnUseBlock;
		}

		public void OnUseBlock(IServerPlayer byPlayer, BlockSelection blockSel)
		{
			Block usedBlock = this.serverApi.World.BlockAccessor.GetBlock(blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z);
			foreach (int bedID in this.bedIDs)
			{
				if (usedBlock.BlockId == bedID)
				{
					PlayerSpawnPos playerPos = new PlayerSpawnPos(blockSel.Position.X, blockSel.Position.Y, blockSel.Position.Z);
					byPlayer.SetSpawnPosition(playerPos);
					byPlayer.SendMessage(GlobalConstants.GeneralChatGroup, "Set new respawn point", EnumChatType.Notification);
					break;
				}
			}
		}
	}
}
