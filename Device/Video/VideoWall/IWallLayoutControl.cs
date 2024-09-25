﻿using MAVF.API.Layout;

namespace MAVF.API.Device.Video.VideoWall
{
	public interface IWallLayoutControl
	{
		public ILayout GetWallLayout(int wall);

		public void SetWallLayout(int wall, ILayout layout);
	}
}
