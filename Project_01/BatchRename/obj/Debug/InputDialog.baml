   M S B A M L   `   `   `  ????F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename  5   	       BatchRename.InputDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BatchRename     5   	   -   5          ? Title$  Preset??5   	    ? SizeToContent$ WidthAndHeight=?6    ? WindowStartupLocation$ CenterScreen=?67   $	??200??5
   	   $	??200??6   .??5      ? $??15q?6    ? ColumnDefinitions 5   
   ?? 5   
   $
??Auto ?6   ?? 5   
   $??* ?6    ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? -   5        $??lblQuestion??5      $??1??6#   .??63   Preset Name:6A   ?? 5   
   -   5        $??	txtAnswer??5      $??1??6#   $??1??63   $	??250??6@   .??6O   Name6U   ? 5   
   $??2??6   $??2??6!   $??Right=?65   $??0,15,0,0q?6Q   .+?5      ?? -   5        $??btnDialogOk??5   \      	TabButton#??[  6   
???   ? ?? 	IsDefault . $??60??6?   $??0,0,10,0q?6?   .??6?   Ok6?   ?? 5      #??[  6   
???   ? ?? IsCancel . $??60??6[   .??6i   Cancel6q   5      5      5      