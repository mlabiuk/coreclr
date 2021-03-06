using System;
using System.Collections.Generic;
using System.Collections;
/// <summary>
///SyncRoot
/// </summary>
public class ListICollectionSyncRoot
{
    #region Public Methods
    public bool RunTests()
    {
        bool retVal = true;

        TestLibrary.TestFramework.LogInformation("[Positive]");
        retVal = PosTest1() && retVal;
        return retVal;
    }

    #region Positive Test Cases
    public bool PosTest1()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest1: this SyncRoot property always returns the current instance.");

        try
        {
            int[] iArray = { 1, 9, 3, 6, 5, 8, 7, 2, 4, 0 };
            List<int> listObject = new List<int>(iArray);
            object actualValue=((ICollection)listObject).SyncRoot;
            if (actualValue==null)
            {
                TestLibrary.TestFramework.LogError("001.1", "calling SyncRoot property should return current instance.");
                retVal = false;
            }
            string [] sArray = { "1", "9", "3", "6", "5", "8", "7", "2", "4", "0" };
            List<string> listObject1 = new List<string>(sArray);
            actualValue = ((ICollection)listObject1).SyncRoot;
            if (actualValue==null)
            {
                TestLibrary.TestFramework.LogError("001.2", "calling SyncRoot property should return current instance.");
                retVal = false;
            }
            
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("001.0", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }

   
    #endregion
    #endregion

    public static int Main()
    {
        ListICollectionSyncRoot test = new ListICollectionSyncRoot();

        TestLibrary.TestFramework.BeginTestCase("ListICollectionSyncRoot");

        if (test.RunTests())
        {
            TestLibrary.TestFramework.EndTestCase();
            TestLibrary.TestFramework.LogInformation("PASS");
            return 100;
        }
        else
        {
            TestLibrary.TestFramework.EndTestCase();
            TestLibrary.TestFramework.LogInformation("FAIL");
            return 0;
        }
    }
}
