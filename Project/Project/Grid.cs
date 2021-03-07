﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Project
{
    public class Grid
    {
        public Node[][] NodeGrid { get; private set; }
        private List<char[]> CharsGrid;
        public Node StartNode { get; private set; }
        public Node EndNode { get; private set; }
        
        public Grid(string path, int xStart, int yStart, int xEnd, int yEnd)
        {
            CharsGrid = new List<char[]>();
            using (var sr= new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    char[] currentRow = new char[(int) Math.Ceiling(row.Length/2.0)];
                    for (int i = 0; i < row.Length; i+=2)
                    {
                        currentRow[i/2] = row[i];
                    }
                    CharsGrid.Add(currentRow);
                }
            }
            ConvertGridToNodeGrid(xStart, yStart, xEnd, yEnd);
        }

       private void ConvertGridToNodeGrid(int xStart, int yStart, int xEnd, int yEnd)
        {
           Node[][] grid = new Node[CharsGrid.Count][];
            for (int i = 0; i < CharsGrid.Count; i++)
            {
                grid[i] = new Node[CharsGrid[i].Length];
                for (int j = 0; j < CharsGrid[i].Length; j++)
                {
                    grid[i][j]=new Node(j, i, CharsGrid[i][j]);
                }
            }
            NodeGrid = grid;
            StartNode = NodeGrid[yStart][xStart];
            EndNode = NodeGrid[yEnd][xEnd];
        }
       
        public void SetWayOnGrid(List<Node> way)
        {
            foreach (var currentNode in way)
            {
                CharsGrid[currentNode.Y][currentNode.X] = '*';
            }
        }

        public void PrintGrid(string path)
        {
            using ( StreamWriter sw = new StreamWriter(path, true))
            {
                for (int i = 0; i < CharsGrid.Count; i++)
                {
                    for (int j = 0; j < CharsGrid[i].Length; j++)
                    {
                        Console.Write(CharsGrid[i][j]+" ");
                        sw.Write(CharsGrid[i][j]+" ");
                    }
                    Console.WriteLine();
                    sw.WriteLine();
                }
            }
        }
    }
}