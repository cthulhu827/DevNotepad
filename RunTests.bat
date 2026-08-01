@echo off
setlocal enabledelayedexpansion
cd /d "%~dp0"

:: Force English output from dotnet CLI + UTF-8 console
set "DOTNET_CLI_UI_LANGUAGE=en"
chcp 65001 >nul 2>nul

echo ============================================
echo   DevNotepad - Run All Unit Tests
echo ============================================
echo.

set "TEST_PROJECTS=DevNotepad.Core.Tests\DevNotepad.Core.Tests.csproj DtConverter.Tests\DtConverter.Tests.csproj"
set "TMP_OUTPUT=%TEMP%\devnotepad_test_output.txt"
set "PASSED=0"
set "FAILED=0"
set "FAILED_TESTS_FILE=%TEMP%\devnotepad_failed_tests.txt"

:: Clear accumulated failed tests file
type nul > "%FAILED_TESTS_FILE%" 2>nul

for %%P in (%TEST_PROJECTS%) do (
    echo ------------------------------------------
    echo   Running: %%~nxP
    echo ------------------------------------------

    dotnet test "%%P" --no-restore --verbosity normal > "%TMP_OUTPUT%" 2>&1
    set "EXIT_CODE=!errorlevel!"

    :: Show full output
    type "%TMP_OUTPUT%"

    if !EXIT_CODE! neq 0 (
        set /a FAILED+=1
        echo [FAIL] %%~nxP

        :: Extract lines with failed tests (NUnit format: "  Failed TestName [time]")
        echo --- Failed tests in %%~nxP: --- >> "%FAILED_TESTS_FILE%"
        findstr /R /C:"  Failed " "%TMP_OUTPUT%" >> "%FAILED_TESTS_FILE%" 2>nul
        echo. >> "%FAILED_TESTS_FILE%"
    ) else (
        set /a PASSED+=1
        echo [PASS] %%~nxP
    )
    echo.
)

echo ============================================
echo   Results: !PASSED! passed, !FAILED! failed
echo ============================================

if !FAILED! gtr 0 (
    echo.
    echo ============================================
    echo   Failed Tests:
    echo ============================================
    type "%FAILED_TESTS_FILE%"
    del "%TMP_OUTPUT%" 2>nul
    del "%FAILED_TESTS_FILE%" 2>nul
    exit /b 1
)

del "%TMP_OUTPUT%" 2>nul
del "%FAILED_TESTS_FILE%" 2>nul
exit /b 0
