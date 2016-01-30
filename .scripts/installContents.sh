#!/bin/bash

if [ "$1" = "" ]; then
  echo "Please specify project directory";
  exit 1;
fi

# define versions of some frameworks we want to use
jqueryVer="2.2.0";
jqueryUiVer="1.11.4";
bootstrapVer="3.3.6";
bootboxVer="4.4.0";
chosenVer="1.0.0"; # this is for bootstrap-chosen

# function to create a dir in case not existing yet
createDir() {
  if [ ! -d "$1" ]; then
    echo "Creating \"$1\"";
    mkdir "$1";
  fi
}

# the target directories
dirProject="$1";

# check for Content directory
dirContent="$dirProject/Sections/Common/Content";
createDir $dirContent;

# check for Content/img directory
dirImg="$dirContent/img";
createDir $dirImg;

# check for Content/img directory
dirFonts="$dirContent/fonts";
createDir $dirFonts;

# check for Content/css directory
dirCss="$dirContent/css";
createDir $dirCss;

# check for Content/less directory
dirLess="$dirContent/less";
createDir $dirLess;

# check for Scripts directory
dirScripts="$dirContent/js";
createDir $dirScripts;

# dnx package directory
dirDnxPackages="$HOME/.dnx/packages";

#########################################
## installing stuff

# get jQuery
jqueryPath="$dirDnxPackages/jQuery/$jqueryVer/Content";
echo "Installing jQUery-$jqueryVer";
if [ -d "$jqueryPath" ]; then
  cp -f -t $dirScripts $jqueryPath/Scripts/jquery-$jqueryVer.js;
  echo "...finished!";
else
  echo "ERROR: Could not install jQuery. The package path does not exist:";
  echo "  $jqueryPath";
fi

# get jQuery-UI
jqueryUiPath="$dirDnxPackages/jQuery.UI.Combined/$jqueryUiVer/Content";
echo "Installing jQUery-UI";
if [ -d "$jqueryUiPath" ]; then
  cp -f -t $dirScripts $jqueryUiPath/Scripts/jquery-ui-$jqueryUiVer.js;
  cp -f -t $dirCss -r $jqueryUiPath/Content/themes;
  echo "...finished!";
else
  echo "ERROR: Could not install jQuery-UI. The package path does not exist:";
  echo "  $jqueryUiPath";
fi

# get bootstrap
bootstrapPath="$dirDnxPackages/Bootstrap.Less/$bootstrapVer/content";
echo "Installing Bootstrap.Less $bootstrapVer...";
if [ -d "$bootstrapPath" ]; then
  cp -f -t $dirLess -r $bootstrapPath/Content/bootstrap;
  cp -f -t $dirFonts $bootstrapPath/Content/fonts/*;
  cp -f -t $dirScripts $bootstrapPath/Scripts/bootstrap.js;
  echo "...finished!";
else
  echo "ERROR: Could not install Boostrap. The package path does not exist:";
  echo "  $bootstrapPath";
fi

# get bootbox
bootboxPath="$dirDnxPackages/Bootbox.JS/$bootboxVer/content";
echo "Installing Bootbox.JS $bootboxVer...";
if [ -d "$bootstrapPath" -a -d "$bootboxPath" ]; then
  cp -f -t $dirScripts $bootboxPath/Scripts/bootbox.js;
  echo "...finished!";
else
  echo "ERROR: Could not install Bootbox.JS. The package path does not exist:";
  echo "  $bootboxPath";
fi

# get bootstrap.chosen
chosenPath="$dirDnxPackages/bootstrap.chosen/$chosenVer/content";
echo "Installing bootstrap-chosen $chosenVer...";
if [ -d "$bootstrapPath" -a -d "$chosenPath" ]; then
  # create bootstrap-chosen directory
  dirLessChosen="$dirLess/chosen";
  createDir "$dirLessChosen";

  cp -f -t $dirLessChosen $chosenPath/Content/bootstrap-chosen.less;
  cp -f -t $dirImg $chosenPath/Content/chosen-sprite.png;
  cp -f -t $dirScripts $chosenPath/Scripts/chosen.jquery.js;
  echo "...finished!";
else
  echo "ERROR: Could not install bootstrap-chosen. The package path does not exist:";
  echo "  $chosenPath";
fi
