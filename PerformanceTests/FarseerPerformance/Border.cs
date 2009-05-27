﻿using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Mathematics;

namespace FarseerPerformance
{
    public class Border
    {
        private Body _borderBody;
        private Geom[] _borderGeom;
        private int _borderWidth;
        private int _height;
        private Vector2 _position;
        private int _width;

        public Border(int width, int height, int borderWidth, Vector2 position)
        {
            _width = width;
            _height = height;
            _borderWidth = borderWidth;
            _position = position;
        }

        public void Load(PhysicsSimulator physicsSimulator)
        {
            //use the body factory to create the physics body
            _borderBody = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, _width, _height, 1);
            _borderBody.IsStatic = true;
            _borderBody.Position = _position;

            LoadBorderGeom(physicsSimulator);
        }

        private void LoadBorderGeom(PhysicsSimulator physicsSimulator)
        {
            _borderGeom = new Geom[4];
            //left border
            Vector2 geometryOffset = new Vector2(-(_width * .5f - _borderWidth * .5f), 0);
            _borderGeom[0] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, _borderBody, _borderWidth,
                                                                      _height,
                                                                      geometryOffset, 0);
            _borderGeom[0].RestitutionCoefficient = .0001f;
            _borderGeom[0].FrictionCoefficient = .5f;
            _borderGeom[0].CollisionGroup = 100;

            //right border (clone left border since geometry is same size)
            geometryOffset = new Vector2(_width * .5f - _borderWidth * .5f, 0);
            _borderGeom[1] = GeomFactory.Instance.CreateGeom(physicsSimulator, _borderBody, _borderGeom[0],
                                                             geometryOffset,
                                                             0);


            //top border
            geometryOffset = new Vector2(0, -(_height * .5f - _borderWidth * .5f));


            _borderGeom[2] = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, _borderBody, _width,
                                                                      _borderWidth,
                                                                      geometryOffset, 0, 20);
            _borderGeom[2].RestitutionCoefficient = .2f;
            _borderGeom[2].FrictionCoefficient = .2f;
            _borderGeom[2].CollisionGroup = 100;

            //bottom border (clone top border since geometry is same size)
            geometryOffset = new Vector2(0, _height * .5f - _borderWidth * .5f);
            _borderGeom[3] = GeomFactory.Instance.CreateGeom(physicsSimulator, _borderBody, _borderGeom[2],
                                                             geometryOffset,
                                                             0);
        }
    }
}