using Godot;
using System;

public delegate void RefAction<T1>(ref T1 obj1);
public delegate void RefAction<T1, T2>(ref T1 obj1, T2 obj2);
public delegate void RefAction<T1, T2, T3>(ref T1 obj1, ref T2 obj2, T3 obj3);
public delegate void RefAction<T1, T2, T3, T4>(ref T1 obj1, ref T2 obj2, ref T3 obj3, T4 obj4);
public delegate void RefAction<T1, T2, T3, T4, T5>(ref T1 obj1, ref T2 obj2, ref T3 obj3, ref T4 obj4, T5 obj5);
