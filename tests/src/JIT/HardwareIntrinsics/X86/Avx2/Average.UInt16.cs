// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void AverageUInt16()
        {
            var test = new SimpleBinaryOpTest__AverageUInt16();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                // Validates basic functionality works, using Load
                test.RunBasicScenario_Load();

                // Validates basic functionality works, using LoadAligned
                test.RunBasicScenario_LoadAligned();

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                // Validates calling via reflection works, using Load
                test.RunReflectionScenario_Load();

                // Validates calling via reflection works, using LoadAligned
                test.RunReflectionScenario_LoadAligned();

                // Validates passing a static member works
                test.RunClsVarScenario();

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                // Validates passing a local works, using Load
                test.RunLclVarScenario_Load();

                // Validates passing a local works, using LoadAligned
                test.RunLclVarScenario_LoadAligned();

                // Validates passing the field of a local works
                test.RunLclFldScenario();

                // Validates passing an instance member works
                test.RunFldScenario();
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class SimpleBinaryOpTest__AverageUInt16
    {
        private const int VectorSize = 32;
        private const int ElementCount = VectorSize / sizeof(UInt16);

        private static UInt16[] _data1 = new UInt16[ElementCount];
        private static UInt16[] _data2 = new UInt16[ElementCount];

        private static Vector256<UInt16> _clsVar1;
        private static Vector256<UInt16> _clsVar2;

        private Vector256<UInt16> _fld1;
        private Vector256<UInt16> _fld2;

        private SimpleBinaryOpTest__DataTable<UInt16> _dataTable;

        static SimpleBinaryOpTest__AverageUInt16()
        {
            var random = new Random();

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (ushort)(random.Next(short.MinValue, short.MaxValue)); _data2[i] = (ushort)(random.Next(short.MinValue, short.MaxValue)); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<UInt16>, byte>(ref _clsVar1), ref Unsafe.As<UInt16, byte>(ref _data2[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<UInt16>, byte>(ref _clsVar2), ref Unsafe.As<UInt16, byte>(ref _data1[0]), VectorSize);
        }

        public SimpleBinaryOpTest__AverageUInt16()
        {
            Succeeded = true;

            var random = new Random();

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (ushort)(random.Next(short.MinValue, short.MaxValue)); _data2[i] = (ushort)(random.Next(short.MinValue, short.MaxValue)); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<UInt16>, byte>(ref _fld1), ref Unsafe.As<UInt16, byte>(ref _data1[0]), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector256<UInt16>, byte>(ref _fld2), ref Unsafe.As<UInt16, byte>(ref _data2[0]), VectorSize);

            for (var i = 0; i < ElementCount; i++) { _data1[i] = (ushort)(random.Next(short.MinValue, short.MaxValue)); _data2[i] = (ushort)(random.Next(short.MinValue, short.MaxValue)); }
            _dataTable = new SimpleBinaryOpTest__DataTable<UInt16>(_data1, _data2, new UInt16[ElementCount], VectorSize);
        }

        public bool IsSupported => Avx2.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            var result = Avx2.Average(
                Unsafe.Read<Vector256<UInt16>>(_dataTable.inArray1Ptr),
                Unsafe.Read<Vector256<UInt16>>(_dataTable.inArray2Ptr)
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_Load()
        {
            var result = Avx2.Average(
                Avx.LoadVector256((UInt16*)(_dataTable.inArray1Ptr)),
                Avx.LoadVector256((UInt16*)(_dataTable.inArray2Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunBasicScenario_LoadAligned()
        {
            var result = Avx2.Average(
                Avx.LoadAlignedVector256((UInt16*)(_dataTable.inArray1Ptr)),
                Avx.LoadAlignedVector256((UInt16*)(_dataTable.inArray2Ptr))
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            var result = typeof(Avx2).GetMethod(nameof(Avx2.Average), new Type[] { typeof(Vector256<UInt16>), typeof(Vector256<UInt16>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector256<UInt16>>(_dataTable.inArray1Ptr),
                                        Unsafe.Read<Vector256<UInt16>>(_dataTable.inArray2Ptr)
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<UInt16>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_Load()
        {
            var result = typeof(Avx2).GetMethod(nameof(Avx2.Average), new Type[] { typeof(Vector256<UInt16>), typeof(Vector256<UInt16>) })
                                     .Invoke(null, new object[] {
                                        Avx.LoadVector256((UInt16*)(_dataTable.inArray1Ptr)),
                                        Avx.LoadVector256((UInt16*)(_dataTable.inArray2Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<UInt16>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunReflectionScenario_LoadAligned()
        {
            var result = typeof(Avx2).GetMethod(nameof(Avx2.Average), new Type[] { typeof(Vector256<UInt16>), typeof(Vector256<UInt16>) })
                                     .Invoke(null, new object[] {
                                        Avx.LoadAlignedVector256((UInt16*)(_dataTable.inArray1Ptr)),
                                        Avx.LoadAlignedVector256((UInt16*)(_dataTable.inArray2Ptr))
                                     });

            Unsafe.Write(_dataTable.outArrayPtr, (Vector256<UInt16>)(result));
            ValidateResult(_dataTable.inArray1Ptr, _dataTable.inArray2Ptr, _dataTable.outArrayPtr);
        }

        public void RunClsVarScenario()
        {
            var result = Avx2.Average(
                _clsVar1,
                _clsVar2
            );

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_clsVar1, _clsVar2, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            var left = Unsafe.Read<Vector256<UInt16>>(_dataTable.inArray1Ptr);
            var right = Unsafe.Read<Vector256<UInt16>>(_dataTable.inArray2Ptr);
            var result = Avx2.Average(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_Load()
        {
            var left = Avx.LoadVector256((UInt16*)(_dataTable.inArray1Ptr));
            var right = Avx.LoadVector256((UInt16*)(_dataTable.inArray2Ptr));
            var result = Avx2.Average(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArrayPtr);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            var left = Avx.LoadAlignedVector256((UInt16*)(_dataTable.inArray1Ptr));
            var right = Avx.LoadAlignedVector256((UInt16*)(_dataTable.inArray2Ptr));
            var result = Avx2.Average(left, right);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(left, right, _dataTable.outArrayPtr);
        }

        public void RunLclFldScenario()
        {
            var test = new SimpleBinaryOpTest__AverageUInt16();
            var result = Avx2.Average(test._fld1, test._fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(test._fld1, test._fld2, _dataTable.outArrayPtr);
        }

        public void RunFldScenario()
        {
            var result = Avx2.Average(_fld1, _fld2);

            Unsafe.Write(_dataTable.outArrayPtr, result);
            ValidateResult(_fld1, _fld2, _dataTable.outArrayPtr);
        }

        public void RunUnsupportedScenario()
        {
            Succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                Succeeded = true;
            }
        }

        private void ValidateResult(Vector256<UInt16> left, Vector256<UInt16> right, void* result, [CallerMemberName] string method = "")
        {
            UInt16[] inArray1 = new UInt16[ElementCount];
            UInt16[] inArray2 = new UInt16[ElementCount];
            UInt16[] outArray = new UInt16[ElementCount];

            Unsafe.Write(Unsafe.AsPointer(ref inArray1[0]), left);
            Unsafe.Write(Unsafe.AsPointer(ref inArray2[0]), right);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<UInt16, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray1, inArray2, outArray, method);
        }

        private void ValidateResult(void* left, void* right, void* result, [CallerMemberName] string method = "")
        {
            UInt16[] inArray1 = new UInt16[ElementCount];
            UInt16[] inArray2 = new UInt16[ElementCount];
            UInt16[] outArray = new UInt16[ElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<UInt16, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(left), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<UInt16, byte>(ref inArray2[0]), ref Unsafe.AsRef<byte>(right), VectorSize);
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<UInt16, byte>(ref outArray[0]), ref Unsafe.AsRef<byte>(result), VectorSize);

            ValidateResult(inArray1, inArray2, outArray, method);
        }

        private void ValidateResult(UInt16[] left, UInt16[] right, UInt16[] result, [CallerMemberName] string method = "")
        {
            if ((ushort)((left[0] + right[0] + 1) >> 1) != result[0])
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < left.Length; i++)
                {
                    if ((ushort)((left[i] + right[i] + 1) >> 1) != result[i])
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                Console.WriteLine($"{nameof(Avx2)}.{nameof(Avx2.Average)}<UInt16>: {method} failed:");
                Console.WriteLine($"    left: ({string.Join(", ", left)})");
                Console.WriteLine($"   right: ({string.Join(", ", right)})");
                Console.WriteLine($"  result: ({string.Join(", ", result)})");
                Console.WriteLine();
            }
        }
    }
}
