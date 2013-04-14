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
using HaCreator.GUI;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace HaCreator.MapEditor
{
    public class Saver
    {

        private static Hashtable tileNums = new Hashtable();
        private static Hashtable objNums = new Hashtable();
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
            //WzImage mapBoard.MapInfo.mapImage = mapBoard.MapInfo.mapImage;
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
            mapBoard.MapInfo.mapImage.RemoveProperty("foothold");
            mapBoard.MapInfo.mapImage.AddProperty(fh);
            mapBoard.MapInfo.mapImage.Changed = true;
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
            newObj.AddProperty(new WzCompressedIntProperty("zM", boardObj.Z));
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
            newObj.AddProperty(new WzCompressedIntProperty("z", boardTile.Z));
            newObj.AddProperty(new WzCompressedIntProperty("zM", boardTile.Z));
            tileParent.ParentImage.Changed = true;
            //return newObj;
        }

        /// <summary>
        /// Removes obj and tile from all layers, then adds back empty ones for saving.
        /// </summary>
        /// <param name="map">Map image that contains the layers</param>
        public static void removeLayerProps(WzImage map)
        {
            for (int i = 0; i <= 7; i++)
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
                else
                {
                    WzSubProperty layer = new WzSubProperty(i.ToString());
                    WzSubProperty objParent = new WzSubProperty("obj");
                    WzSubProperty tileParent = new WzSubProperty("tile");
                    WzSubProperty infoParent = new WzSubProperty("info");
                    layer.AddProperty(infoParent);
                    layer.AddProperty(objParent);
                    layer.AddProperty(tileParent);
                    map.AddProperty(layer);
                }
            }
        }

        /// <summary>
        /// Overwrites the .img being edited, based on the Map open inside the Board
        /// </summary>
        /// <param name="mapBoard">Board containing everything about the map</param>
        public static string WriteToMap(Board mapBoard)
        {
            #region Info
            if (mapBoard.MapInfo.mapImage["info"] != null)
                mapBoard.MapInfo.mapImage["info"].Remove();
            WzSubProperty info = new WzSubProperty("info");
            info.AddProperty(new WzStringProperty("bgm", mapBoard.MapInfo.bgm));
            if (mapBoard.MapInfo.MapMark != null)
                info.AddProperty(new WzStringProperty("mapMark", mapBoard.MapInfo.MapMark));
            else
                info.AddProperty(new WzStringProperty("mapMark", "None"));
            info.AddProperty(new WzCompressedIntProperty("cloud", (MapleBool)mapBoard.MapInfo.cloud));
            info.AddProperty(new WzCompressedIntProperty("version", 10));
            info.AddProperty(new WzCompressedIntProperty("town", (MapleBool)mapBoard.MapInfo.town));
            info.AddProperty(new WzCompressedIntProperty("returnMap", mapBoard.MapInfo.returnMap));
            try
            {
                info.AddProperty(new WzByteFloatProperty("mobRate", mapBoard.MapInfo.mobRate));
            }
            catch
            {
                info.AddProperty(new WzByteFloatProperty("mobRate", 1.5f));
            }
            info.AddProperty(new WzCompressedIntProperty("swim", (MapleBool)mapBoard.MapInfo.swim));
            info.AddProperty(new WzCompressedIntProperty("hideMinimap", (MapleBool)mapBoard.MapInfo.hideMinimap));
            try
            {
                info.AddProperty(new WzCompressedIntProperty("fieldLimit", Convert.ToInt32(mapBoard.MapInfo.fieldLimit)));
            }
            catch
            {
                info.AddProperty(new WzCompressedIntProperty("fieldLimit", 0));
            }
            try
            {
                info.AddProperty(new WzCompressedIntProperty("forcedReturn", mapBoard.MapInfo.forcedReturn));
            }
            catch
            {
                info.AddProperty(new WzCompressedIntProperty("forcedReturn", 999999999));
            }
            info.AddProperty(new WzCompressedIntProperty("VRTop", mapBoard.MapInfo.VR.Value.Y));
            info.AddProperty(new WzCompressedIntProperty("VRLeft", mapBoard.MapInfo.VR.Value.X));
            info.AddProperty(new WzCompressedIntProperty("VRBottom", mapBoard.MapInfo.VR.Value.Height + mapBoard.MapInfo.VR.Value.Y));
            info.AddProperty(new WzCompressedIntProperty("VRRight", mapBoard.MapInfo.VR.Value.Width + mapBoard.MapInfo.VR.Value.X));
            mapBoard.MapInfo.mapImage.AddProperty(info);
            #endregion

            #region Back
            if (mapBoard.MapInfo.mapImage["back"] != null)
                mapBoard.MapInfo.mapImage["back"].Remove();
            WzSubProperty back = new WzSubProperty("back");
            int backcount = 0;
            foreach (BackgroundInstance backInst in mapBoard.BoardItems.BackBackgrounds)
            {
                WzSubProperty backParent = new WzSubProperty(Convert.ToString(backcount));
                backParent.AddProperty(new WzCompressedIntProperty("a", backInst.a));
                backParent.AddProperty(new WzCompressedIntProperty("ani", (MapleBool)((BackgroundInfo)backInst.BaseInfo).ani));
                backParent.AddProperty(new WzStringProperty("bS", ((BackgroundInfo)backInst.BaseInfo).bS));
                backParent.AddProperty(new WzCompressedIntProperty("cx", backInst.cx));
                backParent.AddProperty(new WzCompressedIntProperty("cy", backInst.cy));
                backParent.AddProperty(new WzCompressedIntProperty("f", (MapleBool)backInst.Flip));
                backParent.AddProperty(new WzCompressedIntProperty("front", (MapleBool)backInst.front));
                backParent.AddProperty(new WzCompressedIntProperty("no", Convert.ToInt32(((BackgroundInfo)backInst.BaseInfo).no)));
                backParent.AddProperty(new WzCompressedIntProperty("rx", backInst.rx));
                backParent.AddProperty(new WzCompressedIntProperty("ry", backInst.ry));
                backParent.AddProperty(new WzCompressedIntProperty("type", Convert.ToInt32(backInst.type)));
                backParent.AddProperty(new WzCompressedIntProperty("x", backInst.X));
                backParent.AddProperty(new WzCompressedIntProperty("y", backInst.Y));
                backcount++;
                back.AddProperty(backParent);
            }

            foreach (BackgroundInstance backInst in mapBoard.BoardItems.FrontBackgrounds)
            {
                WzSubProperty backParent = new WzSubProperty(Convert.ToString(backcount));
                backParent.AddProperty(new WzCompressedIntProperty("a", backInst.a));
                backParent.AddProperty(new WzCompressedIntProperty("ani", (MapleBool)((BackgroundInfo)backInst.BaseInfo).ani));
                backParent.AddProperty(new WzStringProperty("bS", ((BackgroundInfo)backInst.BaseInfo).bS));
                backParent.AddProperty(new WzCompressedIntProperty("cx", backInst.cx));
                backParent.AddProperty(new WzCompressedIntProperty("cy", backInst.cy));
                backParent.AddProperty(new WzCompressedIntProperty("f", (MapleBool)backInst.Flip));
                backParent.AddProperty(new WzCompressedIntProperty("front", (MapleBool)backInst.front));
                backParent.AddProperty(new WzCompressedIntProperty("no", Convert.ToInt32(((BackgroundInfo)backInst.BaseInfo).no)));
                backParent.AddProperty(new WzCompressedIntProperty("rx", backInst.rx));
                backParent.AddProperty(new WzCompressedIntProperty("ry", backInst.ry));
                backParent.AddProperty(new WzCompressedIntProperty("type", Convert.ToInt32(backInst.type)));
                backParent.AddProperty(new WzCompressedIntProperty("x", backInst.X));
                backParent.AddProperty(new WzCompressedIntProperty("y", backInst.Y));
                backcount++;
                back.AddProperty(backParent);
            }
            mapBoard.MapInfo.mapImage.AddProperty(back);
            #endregion

            #region Minimap
            if (mapBoard.MapInfo.mapImage["miniMap"] != null)
                mapBoard.MapInfo.mapImage["miniMap"].Remove();
            WzSubProperty mmSub = new WzSubProperty("miniMap");
            WzCanvasProperty canvas = new WzCanvasProperty("canvas");
            canvas.PngProperty = new WzPngProperty();
            canvas.PngProperty.SetPNG(mapBoard.MiniMap);
            mmSub.AddProperty(canvas);
            mmSub.AddProperty(new WzCompressedIntProperty("centerX", mapBoard.CenterPoint.X));
            mmSub.AddProperty(new WzCompressedIntProperty("centerY", mapBoard.CenterPoint.Y));
            mmSub.AddProperty(new WzCompressedIntProperty("height", mapBoard.MapSize.Y));
            mmSub.AddProperty(new WzCompressedIntProperty("width", mapBoard.MapSize.X));
            mmSub.AddProperty(new WzCompressedIntProperty("mag", 4));
            mapBoard.MapInfo.mapImage.AddProperty(mmSub);
            #endregion

            #region ladderRope
            if (mapBoard.MapInfo.mapImage["ladderRope"] != null)
                mapBoard.MapInfo.mapImage["ladderRope"].Remove();
            WzSubProperty ladderRope = new WzSubProperty("ladderRope");
            int ropeCounter = 1;
            foreach (Rope rope in mapBoard.BoardItems.Ropes)
            {
                WzSubProperty ropeParent = new WzSubProperty(Convert.ToString(ropeCounter));
                ropeParent.AddProperty(new WzCompressedIntProperty("l", (MapleBool)rope.ladder));
                ropeParent.AddProperty(new WzCompressedIntProperty("uf", 1));
                ropeParent.AddProperty(new WzCompressedIntProperty("x", rope.FirstAnchor.X));
                if (rope.FirstAnchor.Y < rope.SecondAnchor.Y)
                {
                    ropeParent.AddProperty(new WzCompressedIntProperty("y1", rope.FirstAnchor.Y));
                    ropeParent.AddProperty(new WzCompressedIntProperty("y2", rope.SecondAnchor.Y));
                }
                else
                {
                    ropeParent.AddProperty(new WzCompressedIntProperty("y1", rope.SecondAnchor.Y));
                    ropeParent.AddProperty(new WzCompressedIntProperty("y2", rope.FirstAnchor.Y));
                }
                ropeParent.AddProperty(new WzCompressedIntProperty("page", rope.page));
                ropeCounter++;
                ladderRope.AddProperty(ropeParent);
            }
            mapBoard.MapInfo.mapImage.AddProperty(ladderRope);
            #endregion

            #region Remove/Re-add obj/tile
            removeLayerProps(mapBoard.MapInfo.mapImage);
            #endregion

            #region TileObj
            foreach (LayeredItem tileObj in mapBoard.BoardItems.TileObjs)
            {

                IWzImageProperty curTileObj = mapBoard.MapInfo.mapImage[tileObj.LayerNumber.ToString()];
                if (objNums[tileObj.LayerNumber] == null) objNums[tileObj.LayerNumber] = 0;
                if (tileNums[tileObj.LayerNumber] == null) tileNums[tileObj.LayerNumber] = 0;
                #region Object
                if (tileObj is ObjectInstance)
                {
                    String objName = objNums[tileObj.LayerNumber].ToString();
                    IWzImageProperty curObj = curTileObj["obj"];
                    AddMapObj((WzSubProperty)curObj, objName, tileObj);
                    objNums[tileObj.LayerNumber] = Convert.ToInt32(objNums[tileObj.LayerNumber]) + 1;
                }
                #endregion

                #region Tile
                else if (tileObj is TileInstance)
                {
                    String tileName = tileNums[tileObj.LayerNumber].ToString();
                    IWzImageProperty curTile = curTileObj["tile"];
                    AddMapTile((WzSubProperty)curTile, tileName, tileObj);
                    tileNums[tileObj.LayerNumber] = Convert.ToInt32(tileNums[tileObj.LayerNumber]) + 1;
                }
                #endregion
            }
            #endregion

            #region Vars
            if (mapBoard.MapInfo.mapImage["portal"] != null)
                mapBoard.MapInfo.mapImage["portal"].Remove();
            WzSubProperty portal = new WzSubProperty("portal");
            int portalCount = 0;
            if (mapBoard.MapInfo.mapImage["seat"] != null)
                mapBoard.MapInfo.mapImage["seat"].Remove();
            WzSubProperty seatParent = new WzSubProperty("seat");
            int seatCounter = 0;
            if (mapBoard.MapInfo.mapImage["life"] != null)
                mapBoard.MapInfo.mapImage["life"].Remove();
            WzSubProperty lifeParent = new WzSubProperty("life");
            int lifeCounter = 0;
            if (mapBoard.MapInfo.mapImage["reactor"] != null)
                mapBoard.MapInfo.mapImage["reactor"].Remove();
            WzSubProperty reactorParent = new WzSubProperty("reactor");
            int reactorCounter = 0;
            #endregion

            #region Portal
            foreach (PortalInstance portall in mapBoard.BoardItems.Portals)
            {
                WzSubProperty portalX = new WzSubProperty(Convert.ToString(portalCount));
                portalX.AddProperty(new WzStringProperty("pn", portall.pn));
                portalX.AddProperty(new WzCompressedIntProperty("pt", Convert.ToInt32(portall.pt)));
                portalX.AddProperty(new WzCompressedIntProperty("tm", portall.tm));
                portalX.AddProperty(new WzStringProperty("tn", portall.tn));
                portalX.AddProperty(new WzCompressedIntProperty("x", portall.X));
                portalX.AddProperty(new WzCompressedIntProperty("y", portall.Y));
                if ((!String.IsNullOrEmpty(portall.script)) || (!String.IsNullOrWhiteSpace(portall.script)))
                    portalX.AddProperty(new WzStringProperty("script", portall.script));
                portal.AddProperty(portalX);
                portalCount++;
            }
            #endregion

            #region Life
            #region NPCs
            foreach (LifeInstance npcInst in mapBoard.BoardItems.NPCs)
            {
                WzSubProperty life = new WzSubProperty(Convert.ToString(lifeCounter));
                life.AddProperty(new WzCompressedIntProperty("cy", npcInst.Y));
                life.AddProperty(new WzCompressedIntProperty("y", npcInst.Y));
                life.AddProperty(new WzCompressedIntProperty("x", npcInst.X));
                /*if (((LifeInstance)obj.obj).baseVar is MobInfo && ((MobInfo)((LifeInstance)obj.obj).baseVar).flying)
                    life.AddProperty(new WzCompressedIntProperty("fh", 0));
                else
                {*/
                    FootholdLine fhBelow = FootholdLine.findBelow(new Point(npcInst.X, npcInst.Y), mapBoard);
                    if (fhBelow == null)
                    {
                        Form msgBox = new YesNoBox("No foothold detected", "Auto foothold detection error: No foothold found for the NPC (Name: " + ((NpcInfo)npcInst.BaseInfo).Name + ", ID: " + ((NpcInfo)npcInst.BaseInfo).ID + ") at point " + (npcInst.X).ToString() + "," + (npcInst.Y).ToString() + " (Center-related).\r\nPlease make sure that all Life objects (NPCs/Mobs) are right above a foothold!\r\nClick \"Continue\" if you wish to ignore this problem and continue (could cause map to crash)\r\nClick \"Cancel\" if you would like to cancel saving.", "Continue", "Cancel");
                        if (msgBox.DialogResult != DialogResult.Yes) return "cancel";
                    }
                    else
                    {
                        life.AddProperty(new WzCompressedIntProperty("fh", fhBelow.num));
                    }
                //}
                /*if (((LifeInstance)obj.obj).baseVar is MobInfo)
                {
                    life.AddProperty(new WzCompressedIntProperty("mobTime", Convert.ToInt32(((LifeInstance)npcInst.BaseInfo).MobTime)));
                    life.AddProperty(new WzStringProperty("type", "m"));
                    life.AddProperty(new WzStringProperty("id", ((MobInfo)((LifeInstance)obj.obj).baseVar).ID));
                }
                else
                {*/
                    life.AddProperty(new WzStringProperty("type", "n"));
                    life.AddProperty(new WzStringProperty("id", ((NpcInfo)npcInst.BaseInfo).ID));

                //}
                life.AddProperty(new WzCompressedIntProperty("rx0", npcInst.rx0));
                life.AddProperty(new WzCompressedIntProperty("rx1", npcInst.rx1));
                lifeCounter++;
                lifeParent.AddProperty(life);
            }
            #endregion

            #region Mobs
            foreach (LifeInstance mobInst in mapBoard.BoardItems.Mobs)
            {
                WzSubProperty life = new WzSubProperty(Convert.ToString(lifeCounter));
                life.AddProperty(new WzCompressedIntProperty("cy", mobInst.Y));
                life.AddProperty(new WzCompressedIntProperty("y", mobInst.Y));
                life.AddProperty(new WzCompressedIntProperty("x", mobInst.X));
                //if (mobInst.BaseInfo is MobInfo && ((MobInfo)mobInst.BaseInfo))
                //    life.AddProperty(new WzCompressedIntProperty("fh", 0));
                //else
                //{
                    FootholdLine fhBelow = FootholdLine.findBelow(new Point(mobInst.X, mobInst.Y), mapBoard);
                    if (fhBelow == null)
                    {
                        Form msgBox = new YesNoBox("No foothold detected", "Auto foothold detection error: No foothold found for the Mob (Name: " + ((MobInfo)mobInst.BaseInfo).Name + ", ID: " + ((MobInfo)mobInst.BaseInfo).ID + ") at point " + (mobInst.X).ToString() + "," + (mobInst.Y).ToString() + " (Center-related).\r\nPlease make sure that all Life objects (NPCs/Mobs) are right above a foothold!\r\nClick \"Continue\" if you wish to ignore this problem and continue (could cause map to crash)\r\nClick \"Cancel\" if you would like to cancel saving.", "Continue", "Cancel");
                        if (msgBox.DialogResult != DialogResult.Yes) return "cancel";
                    }
                    life.AddProperty(new WzCompressedIntProperty("fh", fhBelow.fh.num));
                //}
                life.AddProperty(new WzCompressedIntProperty("mobTime", Convert.ToInt32(mobInst.MobTime)));
                life.AddProperty(new WzStringProperty("type", "m"));
                life.AddProperty(new WzStringProperty("id", ((MobInfo)mobInst.BaseInfo).ID));
                life.AddProperty(new WzCompressedIntProperty("rx0", mobInst.rx0));
                life.AddProperty(new WzCompressedIntProperty("rx1", mobInst.rx1));
                lifeCounter++;
                lifeParent.AddProperty(life);
            }
            #endregion
            #endregion

            #region Reactor
            foreach (ReactorInstance reactorInst in mapBoard.BoardItems.Reactors)
            {
                WzSubProperty reactor = new WzSubProperty(Convert.ToString(reactorCounter));
                reactor.AddProperty(new WzCompressedIntProperty("x", reactorInst.X));
                reactor.AddProperty(new WzCompressedIntProperty("y", reactorInst.Y));
                reactor.AddProperty(new WzCompressedIntProperty("f", 0));
                reactor.AddProperty(new WzStringProperty("id", WzInfoTools.AddLeadingZeros(((ReactorInfo)reactorInst.BaseInfo).ID, 7)));
                reactor.AddProperty(new WzCompressedIntProperty("reactorTime", reactorInst.ReactorTime));
                reactor.AddProperty(new WzStringProperty("name", reactorInst.Name));
                reactorCounter++;
                reactorParent.AddProperty(reactor);
            }
            #endregion

            #region Seat/portal check
            #region Portal check
            if (portal.WzProperties.Count == 0)
            {
                Form msgbox = new GUI.YesNoBox("No portals", "No portals were detected in your map.\r\nMaking a map without at least one \"sp\" portal will cause the game to crash.\r\n\r\nClick \"Continue\" to continue building as-is\r\nClick \"Cancel\" to cancel and go back to the editor", "Continue", "Cancel");
                if (msgbox.DialogResult != DialogResult.Yes) return "cancel";
            }
            #endregion
            #region Seat
            foreach (Chair seat in mapBoard.BoardItems.Chairs)
            {
                seatParent.AddProperty(new WzVectorProperty(Convert.ToString(seatCounter), new WzCompressedIntProperty("X", seat.X), new WzCompressedIntProperty("Y", seat.Y)));
                seatCounter++;
            }
            #endregion
            #endregion

            #region Adding P/S/L/R
            mapBoard.MapInfo.mapImage.AddProperty(portal);
            mapBoard.MapInfo.mapImage.AddProperty(seatParent);
            mapBoard.MapInfo.mapImage.AddProperty(lifeParent);
            mapBoard.MapInfo.mapImage.AddProperty(reactorParent);
            #endregion

            #region FootHolds
            saveFootholds(mapBoard);
            #endregion

            return "fine";
        }
    }
}
