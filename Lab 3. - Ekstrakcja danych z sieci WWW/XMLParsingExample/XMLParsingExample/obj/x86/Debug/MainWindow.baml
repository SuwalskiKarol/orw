   M S B A M L   `   `   `  ����  XMLParsingExample"    XMLParsingExample.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  XML Parsing Example��5   	    � Icon$ Images\Tiger-icon.png��6%   $��Verdana)�5   	   $��12(�6    � DataContext 5      �� 5   
      MainWindowViewModel �� Source# [  6   .��5      � $��8q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��1q�6   
���**�� �� CornerRadius$ 6��5      ._�5      �� $��15, 5, 5, 5q�6   $��18(�6-   $��SemiBold"�5      ���#666666$��XML Parsing��5      6-   5      �� 5   
   $��1��6   $��1q�6   
���   �$��0,5,0,0q�5      ._�5      �  5      �� 5      $
��auto �6$   �� 5      $��* �6$   .+�5       �� $��0��6    �� Orientation$ 
Horizontal=�6*   $��Right=�5!      $��6q�69   .+�5"      �� $��Parse XML z XMLDocument��6   $��
0, 0, 5, 0q�5$      ��5#      ��  �� Path$ XMLDocumentLoadCommand�5$      �� 5%      $��Parse XML z XDocument��6   $��
0, 0, 5, 0q�5'      ��5&      �� $ XDocumentLoadCommand�5'      �� 5(      $��	Wyczyść��6   ��5)      �� $ ClearResultCommand�5*      �� 5,      $��1��6   $��1q�6&   
����  �$��5q�5-   ,   ._�5.      �  5/      �� 50      $
��auto �6,   �� 51      $��* �6,   .+�54      �� $��0��6%   $ 
Horizontal=�55   %   $��5q�6>   .+�56      �� $��SemiBold"�6(   	Uczelnia:6I   �� 57      $��
5, 0, 0, 0q�6(   
��� � ���58   !   �� $% StudentInformationObject.School� �� Mode$ OneWay=��� 5:      $��
0, 0, 5, 0q�6(   ,6?   �� 5;      $��SemiBold"�6(   	Wydział:6H   �� 5<      $��
5, 0, 0, 0q�6(   
��� � ���5=   !   �� $) #StudentInformationObject.Department�$ OneWay=�5?      &   System.Windows.Controls.DataGrid  5A      $��1��6#   $��5q�60   	   
IsReadOnly	 . 
   ColumnHeaderHeight$
 30Q�5B   5   ��5C      �� $* $StudentInformationObject.Studentlist�$ OneWay=�5E      5F      5G      5H      5I      5J      