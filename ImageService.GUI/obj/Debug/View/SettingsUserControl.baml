   M S B A M L   `   `   `  ����K  GImageService.GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ImageService.GUIImageService.GUI  5      /    )ImageService.GUI.View.SettingsUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:ImageService.GUI     5        � 	Resources  5	       	  Title �� Property �� Value��%�   &	        �� 5
   
   .�5   
   ��  � ��$ 20(�6/   �� 5       � ��$
 Bold"�65   �� 5       � ��
 ��  �65   �� 5       � ��$ Center=�6<   �� 5       � ��$ Center=�6>   5      5      .��5      � 
������  � ColumnDefinitions 5   
   �� 5      $	��101 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $��35 �6   �� 5      $��* �6   .+�5    
   �� -   5         ��Name$ SettingsLabel��5       $��Settings��6'   $��2��6:   $��Center=�6N   $��Center=�6p   #��[  6�   �� 5!   
   -   5        $ OutputDiractoryLabel��5!      $��Output Diractory:��6.   $��0q�6J   $��1��6U    J� RenderTransformOrigin$	 0,1&�6b   $
��Left=�6~   $��Center=�6�   �� 5"   
   -   5        $ LogNameLabel��5"      $��	Log Name:��6&   $��0q�6:   $��3��6E   $	 0,1&�6R   $
��Left=�6n   $��Center=�6�   �� 5#   
   -   5        $ SourceNameLabel��5#      $��Source Name:��6)   $��0q�6@   $��2��6K   $	 0,1&�6X   $
��Left=�6t   $��Center=�6�   �� 5$   
   -   5        $ ThumbnailSizeLabel��5$      $��Thumbnail Size:��6,   $��0q�6F   $��4��6Q   $	 0,1&�6^   $
��Left=�6z   $��Center=�6�   �� 5%   
   -   5        $ OutputDiractoryPathLabel��5%      $��0q�6W   $��1��6b   $	 0,1&�6o   $
��Left=�6�   $��Center=�6�   $��1��6�   ��62   �� *VM_OutputDirPath+6�   �� 5&   
   -   5        $ SourceNamePathLabel��5&      $��0q�6O   $��2��6Z   $	 0,1&�6g   $
��Left=�6�   $��Center=�6�   $��1��6�   ��6-   �� *VM_SourceName+6�   �� 5'   
   -   5        $ LogNameDataLabel��5'      $��0q�6I   $��3��6T   $	 0,1&�6a   $
��Left=�6}   $��Center=�6�   $��1��6�   ��6*   �� *
VM_LogName+6�   �� 5(   
   -	   5        $ ThumbnailSizeDataLabel��5(      $��0q�6U   $��4��6`   $	 0,1&�6m   $
��Left=�6�   $��Center=�6�   $��1��6�   ��60   �� *VM_ThumbnailSize+6�   �� 5)   
   -
   5         ��Name$ RemoveButton��5)      $��2��6I   $��Remove��6]   $��0q�6n   $��5��6y   ��6'   �� *RemoveCommand+6y   �� 5*   
   -   5         ��Name$ listBox��5*      
������ $��2��6e   $��0q�6y   $��6��6�   	 �� SelectedItem	 6<   �� *VM_SelectedItem+��6�   �� *
VM_Folders+��5+      �� 5,      .R�5-      �� .+�5.      �� ��6$   �� 5/      50      52      53      54      