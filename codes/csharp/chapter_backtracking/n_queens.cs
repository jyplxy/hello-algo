﻿/**
 * File: n_queens.cs
 * Created Time: 2023-05-04
 * Author: hpstory (hpstory1024@163.com)
 */

using hello_algo.utils;
using NUnit.Framework;

namespace hello_algo.chapter_backtracking;

public class n_queens {
    /* 回溯算法：N 皇后 */
    static void backtrack(int row, int n, List<List<string>> state, List<List<List<string>>> res,
            bool[] cols, bool[] diags1, bool[] diags2) {
        // 当放置完所有行时，记录解
        if (row == n) {
            List<List<string>> copyState = new List<List<string>>();
            foreach (List<string> sRow in state) {
                copyState.Add(new List<string>(sRow));
            }
            res.Add(copyState);
            return;
        }
        // 遍历所有列
        for (int col = 0; col < n; col++) {
            // 计算该格子对应的主对角线和副对角线
            int diag1 = row - col + n - 1;
            int diag2 = row + col;
            // 剪枝：不允许该格子所在 (列 或 主对角线 或 副对角线) 包含皇后
            if (!(cols[col] || diags1[diag1] || diags2[diag2])) {
                // 尝试：将皇后放置在该格子
                state[row][col] = "Q";
                cols[col] = diags1[diag1] = diags2[diag2] = true;
                // 放置下一行
                backtrack(row + 1, n, state, res, cols, diags1, diags2);
                // 回退：将该格子恢复为空位
                state[row][col] = "#";
                cols[col] = diags1[diag1] = diags2[diag2] = false;
            }
        }
    }

    /* 求解 N 皇后 */
    static List<List<List<string>>> nQueens(int n) {
        // 初始化 n*n 大小的棋盘，其中 'Q' 代表皇后，'#' 代表空位
        List<List<string>> state = new List<List<string>>();
        for (int i = 0; i < n; i++) {
            List<string> row = new List<string>();
            for (int j = 0; j < n; j++) {
                row.Add("#");
            }
            state.Add(row);
        }
        bool[] cols = new bool[n]; // 记录列是否有皇后
        bool[] diags1 = new bool[2 * n - 1]; // 记录主对角线是否有皇后
        bool[] diags2 = new bool[2 * n - 1]; // 记录副对角线是否有皇后
        List<List<List<string>>> res = new List<List<List<string>>>();

        backtrack(0, n, state, res, cols, diags1, diags2);

        return res;
    }

    [Test]
    public void Test() {
        int n = 4;
        List<List<List<string>>> res = nQueens(n);

        Console.WriteLine("输入棋盘长宽为 " + n);
        Console.WriteLine("皇后放置方案共有 " + res.Count + " 种");
        foreach (List<List<string>> state in res) {
            Console.WriteLine("--------------------");
            foreach (List<string> row in state) {
                PrintUtil.PrintList(row);
            }
        }
    }
}
