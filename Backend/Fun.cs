using System;
using System.Collections.Generic;

namespace DasJott.Fun {
  public class GetRandom {
    private static Random _random = new Random();
    public static string OK {
      get {
        return "";
      }
    }

    public static string Welcome(string user) {
      return string.Format(GetString(_welcome), user);
    }

    public static List<string> _fortunes = new List<string>();
    public static List<string> _welcome = new List<string>();

    private static string GetString(List<string> list) {
      if (list.Count > 0) {
        return list[_random.Next(list.Count)];
      }
      return "";
    }
  }
}