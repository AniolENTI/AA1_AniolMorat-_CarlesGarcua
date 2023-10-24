using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotController
{

    public struct MyQuat
    {

        public float w;
        public float x;
        public float y;
        public float z;
    }

    public struct MyVec
    {

        public float x;
        public float y;
        public float z;
    }






    public class MyRobotController
    {

        #region public methods


        public string Hi()
        {

            string s = "hello world from my Robot Controller";
            return s;

        }


        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            //Define rotation angle axis
            MyVec rotationAxis;

            //First angle to rotate in: y
            rotationAxis.x = 0;
            rotationAxis.y = 1;
            rotationAxis.z = 0;

            //Rotate the joint 0 to position 75
            rot0 = NullQ; //Need to give this value, if I don't it's not defined and it doesn't work
            rot0 = Rotate(rot0, rotationAxis, 75);


            //Second angle to rotate in: x
            rotationAxis.x = 1;
            rotationAxis.y = 0;

            //Rotate the joints 1, 2 and 3 to the respective positions in order to put it straight
            rot1 = Rotate(rot0, rotationAxis, -7);
            rot2 = Rotate(rot1, rotationAxis, 80);
            rot3 = Rotate(rot2, rotationAxis, 40);
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {
            //Define rotation angle axis
            MyVec rotationAxis;

            float interpolationValue = 0;

            bool myCondition = false;
            //todo: add a check for your condition

            

            if (myCondition)
            {
                //First angle to rotate in: y
                rotationAxis.x = 0;
                rotationAxis.y = 1;
                rotationAxis.z = 0;

                //Rotate joint 0 to position
                rot0 = NullQ; //Need to give this value, if I don't it's not defined and it doesn't work
                rot0 = Rotate(rot0, rotationAxis, CalculateLerp(74, 40, interpolationValue));

                //Second axis to rotate in: x
                rotationAxis.x = 1;
                rotationAxis.y = 0;


                //Rotate each joint to each position
                rot1 = Rotate(rot0, rotationAxis, CalculateLerp(-7, 20, interpolationValue));
                rot2 = Rotate(rot1, rotationAxis, CalculateLerp(80, 39, interpolationValue));
                rot3 = Rotate(rot2, rotationAxis, CalculateLerp(40, 35, interpolationValue));

                interpolationValue += 0.0035f;

                return true;
            }

            //todo: remove this once your code works.
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        //EX3: this function will calculate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.
        //the only difference wtih exercise 2 is that rot3 has a swing and a twist, where the swing will apply to joint3 and the twist to joint4

        public bool PickStudAnimVertical(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            bool myCondition = false;
            //todo: add a check for your condition



            while (myCondition)
            {
                //todo: add your code here


            }

            //todo: remove this once your code works.
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        public static MyQuat GetSwing(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }


        public static MyQuat GetTwist(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }




        #endregion


        #region private and internal methods

        internal int TimeSinceMidnight { get { return (DateTime.Now.Hour * 3600000) + (DateTime.Now.Minute * 60000) + (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond; } }


        private static MyQuat NullQ
        {
            get
            {
                MyQuat a;
                a.w = 1;
                a.x = 0;
                a.y = 0;
                a.z = 0;
                return a;

            }
        }

        internal MyQuat Multiply(MyQuat q1, MyQuat q2)
        {

            MyQuat result;
            result.w = (q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z);
            result.x = (q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y);
            result.y = (q1.w * q2.y + q1.x * q2.z + q1.y * q2.w - q1.z * q2.x);
            result.z = (q1.w * q2.z + q1.x * q2.y + q1.y * q2.x - q1.z * q2.w);

            return result;

        }

        internal MyQuat Rotate(MyQuat currentRotation, MyVec axis, float angle)
        {

            MyQuat result;

            result.w = (float)Math.Cos((angle / 2) * Math.PI / 180);
            result.x = axis.x * (float)Math.Sin((angle / 2) * Math.PI / 180);
            result.y = axis.y * (float)Math.Sin((angle / 2) * Math.PI / 180);
            result.z = axis.z * (float)Math.Sin((angle / 2) * Math.PI / 180);

            result = Multiply(currentRotation, result);

            return result;

        }

        internal float CalculateLerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }



        //todo: add here all the functions needed

        #endregion
    }
}
