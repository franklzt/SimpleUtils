﻿// Classes and structures being serialized

// Generated by ProtocolBuffer
// - a pure c# code generation implementation of protocol buffers
// Report bugs to: https://silentorbit.com/protobuf/

// DO NOT EDIT
// This file will be overwritten when CodeGenerator is run.
// To make custom modifications, edit the .proto file and add //:external before the message line
// then write the code and the changes in a separate file.
using System;
using System.Collections.Generic;

namespace Game
{
    public partial class Login
    {
        public string UserSName { get; set; }

        public string PassWord { get; set; }

    }

    public partial class LoginResult
    {
        public int Result { get; set; }

    }

}