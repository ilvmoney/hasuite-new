using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using System.Collections;
using HaCreator.MapEditor;
using HaCreator.WzStructure;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;
using HaRepackerLib;

namespace HaCreator.MapEditor
{
    public class Saver
    {

        private static int tileNum = 0;
        private static int objNum = 0;
        public static void checkConnected(Board mapBoard)
        {
            /*\
             * This is not used yet, I was making it for putting the FHs in separate groups
             * but lost my train of thought -DeathRight
            \*/
            foreach (FootholdLine fhline in mapBoard.BoardItems.FootholdLines)
            {
                foreach (FootholdLine connectedLine in fhline.FirstDot.connectedLines)
                {

                }
            }
        }

        private static FootholdAnchor GetOtherPoint(FootholdLine source, MapleDot point)
        {
            //This was ripped straight from HC v1.7 and edited to work with v2 -DeathRight
            if (((FootholdAnchor)source.FirstDot).X != point.X || ((FootholdAnchor)source.FirstDot).Y != point.Y)
                return (FootholdAnchor)source.FirstDot;
            else if (((FootholdAnchor)source.SecondDot).X != point.X || ((FootholdAnchor)source.SecondDot).Y != point.Y)
                return (FootholdAnchor)source.SecondDot;
            else
                throw new Exception();
        }

        private static FootholdLine GetOtherFH(List<MapleLine> source, int num)
        {
            //This was ripped straight from HC v1.7 and edited to work with v2 -DeathRight
            foreach (FootholdLine fh in source)
            {
                if (fh.num != num)
                {
                    return fh;
                }
            }
            throw new Exception();
        }

        private static void createFHProps(WzSubProperty fh, MapleDot FirstDot, MapleDot SecondDot)
        {
            fh.AddProperty(new WzCompressedIntProperty("x1", FirstDot.X));
            fh.AddProperty(new WzCompressedIntProperty("x2", SecondDot.X));
            fh.AddProperty(new WzCompressedIntProperty("y1", FirstDot.Y));
            fh.AddProperty(new WzCompressedIntProperty("y2", SecondDot.Y));
        }

        /// <summary>
        /// Saves the footholds to the current map image
        /// </summary>
        /// <param name="mapBoard"></param>
        public static void saveFootholds(Board mapBoard)
        {
            //This was ripped straight from HC v1.7 and edited to work with v2 -DeathRight

            #region properties
            WzImage mainMap = mapBoard.MapInfo.mapImage;
            WzSubProperty fh = new WzSubProperty("foothold");
            Hashtable footholdLayers = new Hashtable(); //Why hashtable? -DeathRight
            #region Stuff that was stupid and commented out
            //WzSubProperty fh2 = new WzSubProperty("4");
            //WzSubProperty fh3 = new WzSubProperty("0");
            /*\
             * ^^^^ I'm guessing that was for testing purposes, looks like haha was
             * making 2 groups (i'm assuming groups), and then commented it out later
             * because he was done testing, though I'm curious as to WHY he used that
             * in the first place.. -DeathRight
            \*/

            /*Hashtable prevTable = new Hashtable();
            Hashtable nextTable = new Hashtable();*/
            #endregion
            int fhnum = 1;
            #endregion

            #region Checking for lines that are only connected to eachother; removing them

            foreach (FootholdLine foothold in mapBoard.BoardItems.FootholdLines)
            {
                foothold.num = fhnum;
                if (foothold.FirstDot.connectedLines.Count > 2)
                {
                    foreach (FootholdLine foothold2 in foothold.FirstDot.connectedLines)
                    {
                        if (foothold2.FirstDot.connectedLines.Count == 1 || foothold2.SecondDot.connectedLines.Count == 1)
                        {
                            foothold.FirstDot.connectedLines.Remove(foothold2);
                            break;
                        }
                    }
                }
                if (foothold.SecondDot.connectedLines.Count > 2)
                {
                    foreach (FootholdLine foothold2 in foothold.SecondDot.connectedLines)
                    {
                        if (foothold2.FirstDot.connectedLines.Count == 1 || foothold2.SecondDot.connectedLines.Count == 1)
                        {
                            foothold.SecondDot.connectedLines.Remove(foothold2);
                            break;
                        }
                    }
                }
                fhnum++;
            }
            #endregion

            #region Saving the footholds
            foreach (FootholdLine foothold in mapBoard.BoardItems.FootholdLines)
            {
                WzSubProperty fhDirectory = null;
                #region Adding 1 FH group to hold FHs. (I'll figure out a better way later)
                if (footholdLayers[foothold.FirstDot.GetLayer()] == null)
                {
                    footholdLayers[foothold.FirstDot.GetLayer()] = new WzSubProperty(foothold.FirstDot.GetLayer().ToString());
                    ((WzSubProperty)footholdLayers[foothold.FirstDot.GetLayer()]).AddProperty(new WzSubProperty("0"));
                }
                fhDirectory = (WzSubProperty)((WzSubProperty)footholdLayers[foothold.FirstDot.GetLayer()])["0"];
                #endregion

                #region Adding FH properties
                WzSubProperty fhprop = new WzSubProperty(Convert.ToString(foothold.num));
                if (foothold.FirstDot.X <= foothold.SecondDot.X)
                {
                    #region old add properties, just incase
                    /*
                    fhprop.AddProperty(new WzCompressedIntProperty("x1", foothold.FirstDot.X));
                    fhprop.AddProperty(new WzCompressedIntProperty("x2", foothold.SecondDot.X));
                    fhprop.AddProperty(new WzCompressedIntProperty("y1", foothold.FirstDot.Y));
                    fhprop.AddProperty(new WzCompressedIntProperty("y2", foothold.SecondDot.Y));
                    */
                    #endregion
                    createFHProps(fhprop, foothold.FirstDot, foothold.SecondDot);
                }
                else
                {
                    createFHProps(fhprop, foothold.SecondDot, foothold.FirstDot);
                }
                #endregion

                #region Figuring out prev/next property
                int prev = 0, next = 0;
                if (foothold.FirstDot.X < foothold.SecondDot.X)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.FirstDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.SecondDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else if (foothold.FirstDot.X > foothold.SecondDot.X)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.SecondDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.FirstDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else if (foothold.SecondDot.connectedLines.Count == 1 || foothold.FirstDot.connectedLines.Count == 1)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.FirstDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.SecondDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else if (GetOtherPoint(GetOtherFH(foothold.FirstDot.connectedLines, foothold.num), foothold.FirstDot).X < GetOtherPoint(GetOtherFH(foothold.SecondDot.connectedLines, foothold.num), foothold.SecondDot).X)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.FirstDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.SecondDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else if (GetOtherPoint(GetOtherFH(foothold.FirstDot.connectedLines, foothold.num), foothold.FirstDot).X > GetOtherPoint(GetOtherFH(foothold.SecondDot.connectedLines, foothold.num), foothold.SecondDot).X)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.SecondDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.FirstDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else if (foothold.FirstDot.Y < foothold.SecondDot.Y)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.FirstDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.SecondDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else if (foothold.FirstDot.Y > foothold.SecondDot.Y)
                {
                    foreach (FootholdLine possiblePrevFH in foothold.SecondDot.connectedLines)
                        if (possiblePrevFH.num != foothold.num)
                            prev = possiblePrevFH.num;
                    foreach (FootholdLine possibleNextFH in foothold.FirstDot.connectedLines)
                        if (possibleNextFH.num != foothold.num)
                            next = possibleNextFH.num;
                }
                else
                {
                    //MessageBox.Show("Foothold with size 0 found, aborting");
                    new GUI.ErrorBox("Foothold with size 0 found, aborting");
                    return;
                }
                #endregion
                fhprop.AddProperty(new WzCompressedIntProperty("prev", prev));
                fhprop.AddProperty(new WzCompressedIntProperty("next", next));
                fhDirectory.AddProperty(fhprop);
            }
            #endregion

            foreach (DictionaryEntry fhLayer in footholdLayers)
            {
                fh.AddProperty((WzSubProperty)fhLayer.Value);
            }
            mainMap.RemoveProperty("foothold");
            mainMap.AddProperty(fh);
            mainMap.Changed = true;
        }

        /// <summary>
        /// Overwrites a (mapid).img object property
        /// </summary>
        /// <param name="obj">The object that is being edited</param>
        /// <param name="boardObj">The Board object that has the edited properties</param>
        public static void overWriteObj(WzSubProperty obj, LayeredItem boardObj)
        {
            if (obj != null)
            {
                ((WzStringProperty)obj["oS"]).Value = ((ObjectInfo)boardObj.BaseInfo).oS;
                ((WzStringProperty)obj["l0"]).Value = ((ObjectInfo)boardObj.BaseInfo).l0;
                ((WzStringProperty)obj["l1"]).Value = ((ObjectInfo)boardObj.BaseInfo).l1;
                ((WzStringProperty)obj["l2"]).Value = ((ObjectInfo)boardObj.BaseInfo).l2;
                ((WzCompressedIntProperty)obj["x"]).Value = boardObj.X;
                ((WzCompressedIntProperty)obj["y"]).Value = boardObj.Y;
                ((WzCompressedIntProperty)obj["z"]).Value = boardObj.Z;
                obj.ParentImage.Changed = true;
            }
        }

        /// <summary>
        /// Overwrites a (mapid).img tile property
        /// </summary>
        /// <param name="tile">The tile that is being edited</param>
        /// <param name="boardTile">The Board tile that has the edited properties</param>
        public static void overWriteTile(WzSubProperty tile, LayeredItem boardTile)
        {
            //Yes, I know I could have easily made overWriteObj check for if it's a tile or obj instance
            //but this class needs more methods in it -DeathRight
            if (tile != null)
            {
                //OOOOOOH, I JUST GOT THAT, 'tS' stands for Tile Source, and 'oS' stands for Object Source
                //better put that in the Tile/ObjectInfo property summarys! -DeathRight
                ((WzStringProperty)((WzSubProperty)tile.Parent.Parent)["info"]["tS"]).Value = ((TileInfo)boardTile.BaseInfo).tS;
                ((WzStringProperty)tile["u"]).Value = ((TileInfo)boardTile.BaseInfo).u;
                ((WzCompressedIntProperty)tile["no"]).Value = Int32.Parse(((TileInfo)boardTile.BaseInfo).no);
                ((WzCompressedIntProperty)tile["x"]).Value = boardTile.X;
                ((WzCompressedIntProperty)tile["y"]).Value = boardTile.Y;
                //Wtf? There's a Z but it's not a property in tiles? Well, not needed I guess -DeathRight
                tile.ParentImage.Changed = true;
            }
        }

        /// <summary>
        /// Adds a map object to the layer
        /// </summary>
        /// <param name="objParent">Parent of the new object</param>
        /// <param name="name">Name of the new object</param>
        /// <param name="boardObj">Board object that contains the new properties</param>
        public static void AddMapObj(WzSubProperty objParent, String name, LayeredItem boardObj)
        {
            WzSubProperty newObj = new WzSubProperty(name);
            //newObj.Parent = objParent;
            objParent.AddProperty(newObj);
            newObj.AddProperty(new WzStringProperty("oS", ((ObjectInfo)boardObj.BaseInfo).oS));
            newObj.AddProperty(new WzStringProperty("l0", ((ObjectInfo)boardObj.BaseInfo).l0));
            newObj.AddProperty(new WzStringProperty("l1", ((ObjectInfo)boardObj.BaseInfo).l1));
            newObj.AddProperty(new WzStringProperty("l2", ((ObjectInfo)boardObj.BaseInfo).l2));
            newObj.AddProperty(new WzCompressedIntProperty("x", boardObj.X));
            newObj.AddProperty(new WzCompressedIntProperty("y", boardObj.Y));
            newObj.AddProperty(new WzCompressedIntProperty("z", boardObj.Z));
            objParent.ParentImage.Changed = true;
            //return newObj;
        }

        /// <summary>
        /// Adds a map tile to the layer
        /// </summary>
        /// <param name="tileParent">Parent of the new tile</param>
        /// <param name="name">Name of the new tile</param>
        /// <param name="boardTile">Board tile that contains the new properties</param>
        // /// <returns>The new object</returns>
        public static void AddMapTile(WzSubProperty tileParent, String name, LayeredItem boardTile)
        {
            WzSubProperty newObj = new WzSubProperty(name);
            //newObj.Parent = tileParent;
            tileParent.AddProperty(newObj);
            if (((WzSubProperty)tileParent.Parent)["info"]["tS"] == null)
                ((WzSubProperty)((WzSubProperty)tileParent.Parent)["info"]).AddProperty(new WzStringProperty("tS", ((TileInfo)boardTile.BaseInfo).tS));
            else
                ((WzStringProperty)((WzSubProperty)tileParent.Parent)["info"]["tS"]).Value = ((TileInfo)boardTile.BaseInfo).tS;
            newObj.AddProperty(new WzStringProperty("u", ((TileInfo)boardTile.BaseInfo).u));
            newObj.AddProperty(new WzCompressedIntProperty("no", Int32.Parse(((TileInfo)boardTile.BaseInfo).no)));
            newObj.AddProperty(new WzCompressedIntProperty("x", boardTile.X));
            newObj.AddProperty(new WzCompressedIntProperty("y", boardTile.Y));
            tileParent.ParentImage.Changed = true;
            //return newObj;
        }

        /// <summary>
        /// Removes obj and tile from all layers, then adds back empty ones for saving.
        /// </summary>
        /// <param name="map">Map image that contains the layers</param>
        public static void removeLayerProps(WzImage map)
        {
            for (int i = 0; i < 11; i++)
            {
                if (map[i.ToString()] != null)
                {
                    WzSubProperty layer = (WzSubProperty)map[i.ToString()];
                    layer["obj"].Remove();
                    layer["tile"].Remove();
                    WzSubProperty objParent = new WzSubProperty("obj");
                    WzSubProperty tileParent = new WzSubProperty("tile");
                    layer.AddProperty(objParent);
                    layer.AddProperty(tileParent);
                }
            }
        }

        /// <summary>
        /// Overwrites the .img being edited, based on the Map open inside the Board
        /// </summary>
        /// <param name="mapBoard">Board containing everything about the map</param>
        public static void WriteToMap(Board mapBoard)
        {

            #region Remove/Re-add obj/tile
            removeLayerProps(mapBoard.MapInfo.mapImage);
            #endregion

            #region TileObj
            foreach (LayeredItem tileObj in mapBoard.BoardItems.TileObjs)
            {

                IWzImageProperty curTileObj = mapBoard.MapInfo.mapImage[tileObj.LayerNumber.ToString()];
                #region Object
                if (tileObj is ObjectInstance)
                {
                    String objName = objNum.ToString();
                    IWzImageProperty curObj = curTileObj["obj"];
                    AddMapObj((WzSubProperty)curObj, objName, tileObj);
                    objNum++;
                }
                #endregion

                #region Tile
                else if (tileObj is TileInstance)
                {
                    String tileName = tileNum.ToString();
                    IWzImageProperty curTile = curTileObj["tile"];
                    AddMapTile((WzSubProperty)curTile, tileName, tileObj);
                    tileNum++;
                }
                #endregion
            }
            #endregion

            #region FootHolds
            saveFootholds(mapBoard);
            #endregion
        }
    }
}
