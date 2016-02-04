using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Mvc.ViewFeatures;

namespace DasJott.UI
{
  public class Modal : IDisposable
  {
    private HtmlHelper Helper;

    public Options Opts { get; private set; }

    public Modal(HtmlHelper helper, Options opts)
    {
      this.Helper = helper;
      this.Opts = opts;
    }

    public class Options
    {
      public string Id = "Modal";
      public string Title = "";
      public bool Titled = true;
      public bool Footer = false;
      public bool Wide = false;
      public bool XButton = true;
      public string CloseButton = "Close";
      public IDictionary<string, string> FooterButtons = new Dictionary<string, string>();
    }

    public static Modal Dialog(HtmlHelper helper, string id, string closeBtn=null, IDictionary<string, string> buttons=null)
    {
      var opts = new Options();

      opts.Id = id;
      opts.Titled = false;
      opts.CloseButton = closeBtn;
      if (opts.CloseButton != null) {
        opts.Footer = true;
      }
      if (buttons != null && buttons.Count > 0) {
        opts.Footer = true;
      }

      return CreateModal(helper, opts);
    }

    public static Modal WideDialog(HtmlHelper helper, string id, string closeBtn=null, IDictionary<string, string> buttons=null)
    {
      var opts = new Options();

      opts.Id = id;
      opts.Titled = false;
      opts.CloseButton = closeBtn;
      if (opts.CloseButton != null) {
        opts.Footer = true;
      }
      if (buttons != null && buttons.Count > 0) {
        opts.Footer = true;
      }
      opts.Wide = true;

      return CreateModal(helper, opts);
    }

    public static Modal TitledDialog(HtmlHelper helper, string id, string title, string closeBtn=null, IDictionary<string, string> buttons=null)
    {
      var opts = new Options();

      opts.Id = id;
      opts.Titled = true;
      opts.Title = title;
      opts.CloseButton = closeBtn;
      if (opts.CloseButton != null) {
        opts.Footer = true;
      }
      if (buttons != null && buttons.Count > 0) {
        opts.Footer = true;
      }

      return CreateModal(helper, opts);
    }

    public static Modal TitledWideDialog(HtmlHelper helper, string id, string title, string closeBtn=null, IDictionary<string, string> buttons=null)
    {
      var opts = new Options();

      opts.Id = id;
      opts.Titled = true;
      opts.Title = title;
      opts.CloseButton = closeBtn;
      if (opts.CloseButton != null) {
        opts.Footer = true;
      }
      if (buttons != null && buttons.Count > 0) {
        opts.Footer = true;
      }
      opts.Wide = true;

      return CreateModal(helper, opts);
    }

    ///////////////////////////////////////////////////////////////////////////

    public static Modal CreateModal(HtmlHelper helper, Options opts)
    {
      var html = new StringBuilder();

      html.AppendFormat("<div class=\"modal modal-wide fade\" id=\"{0}\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"{0}Title\">", opts.Id);
      html.Append("<div class=\"modal-dialog\" role=\"document\">");
      html.Append("<div class=\"modal-content\">");
      if (opts.Titled) {
        html.Append("<div class=\"modal-header\">");
        if (opts.XButton) {
          html.Append("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>");
        }
        html.AppendFormat("<h4 class=\"modal-title\" id=\"{0}Title\">{1}</h4>", opts.Id, opts.Title);
        html.Append("</div>");
      }
      html.AppendFormat("<div class=\"modal-body\" id=\"{0}Body\">", opts.Id);
      if (opts.XButton && !opts.Titled) {
        html.Append("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>");
      }

      helper.ViewContext.Writer.Write(html.ToString());

      return new Modal(helper, opts);
    }

    ///////////////////////////////////////////////////////////////////////////

    public void Dispose()
    {
      var html = new StringBuilder();

      html.Append("</div>"); // modal-body
      if (Opts.Footer) {
        html.Append("<div class=\"modal-footer\">");
        int count = 0, last = Opts.FooterButtons.Count;
        foreach (var btn in Opts.FooterButtons) {
          ++count;
          var btnClass = "btn";
          if (Opts.CloseButton != null || count < last) {
            btnClass += " btn-default";
          } else {
            btnClass += " btn-primary";
          }
          html.AppendFormat("<button type=\"button\" class=\"{0}\" id=\"{1}\">{2}</button>", btnClass, btn.Key, btn.Value);
        }
        if (Opts.CloseButton != null) {
          html.AppendFormat("<button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">{0}</button>", Opts.CloseButton);
        }
        html.Append("</div>");
      }
      html.Append("</div>"); // modal-content
      html.Append("</div>"); // modal-dialog
      html.Append("</div>"); // modal

      Helper.ViewContext.Writer.Write(html.ToString());
    }
  }
}
