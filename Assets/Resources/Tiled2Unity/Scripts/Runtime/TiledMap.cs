using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEditor;

namespace Tiled2Unity
{
    [ExecuteInEditMode]
    public class TiledMap : MonoBehaviour
    {
        [ReadOnly] public int NumTilesWide = 0;
        [ReadOnly] public int NumTilesHigh = 0;
        [ReadOnly] public int TileWidth = 0;
        [ReadOnly] public int TileHeight = 0;
        [ReadOnly] public float ExportScale = 1.0f;

        // Note: Because maps can be isometric and staggered we simply can't multply tile width (or height) by number of tiles wide (or high) to get width (or height)
        // We rely on the exporter to calculate the width and height of the map
        [ReadOnly] public int MapWidthInPixels = 0;
        [ReadOnly] public int MapHeightInPixels = 0;
        [ReadOnly] public int MapID;

        public float GetMapWidthInPixelsScaled()
        {
            return this.MapWidthInPixels * this.transform.lossyScale.x * this.ExportScale;
        }

        public float GetMapHeightInPixelsScaled()
        {
            return this.MapHeightInPixels * this.transform.lossyScale.y * this.ExportScale;
        }


        private void OnDrawGizmosSelected()
        {
            Vector2 pos_w = this.gameObject.transform.position;
            Vector2 topLeft = Vector2.zero + pos_w;
            Vector2 topRight = new Vector2(GetMapWidthInPixelsScaled(), 0) + pos_w;
            Vector2 bottomRight = new Vector2(GetMapWidthInPixelsScaled(), -GetMapHeightInPixelsScaled()) + pos_w;
            Vector2 bottomLeft = new Vector2(0, -GetMapHeightInPixelsScaled()) + pos_w;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        public void OnDestroy() {
            if (Application.isEditor && !EditorApplication.isPlaying) {
                IDHandler.removeMapID(EditorApplication.currentScene, this.MapID);
                
            }
        }
    }
}
